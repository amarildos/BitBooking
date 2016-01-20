using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitBooking.DAL.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace BitBooking.API.Controllers
{
    public class AccomodationFacilitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        // GET: AccomodationFacilities
        /// <summary>
        /// Get list of accommodation FACILITIES for the registered seller
        /// </summary>
        /// <returns>List of Accommodation Facilities</returns>
        public ActionResult Index()
        {

            db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;

            var accomodationFacilities = db.AccomodationFacilities.Where(x=>x.AccomodationId==accID);

            return Json(accomodationFacilities.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: AccomodationFacilities/Details/5
        /// <summary>
        /// Get accommodation FACILITY by AccomodationFacility ID
        /// </summary>
        /// <returns>Accommodation Facilities Object</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationFacility accomodationFacility = db.AccomodationFacilities.Find(id);
            if (accomodationFacility == null)
            {
                return HttpNotFound();
            }
            return View(accomodationFacility);
        }

        // GET: AccomodationFacilities/Create
        /// <summary>
        /// Create accommodation FACILITIES (get creation View())
        /// </summary>
        /// <returns>Create Facilities View</returns>
        public ActionResult Create()
        {
            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName");
            return View();
        }

        // POST: AccomodationFacilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create Accommodation Facility
        /// </summary>
        /// <param name="accomodationFacility">AccommodationFacility object</param>
        /// <returns>View - Index (redirects to Accommodation Facility Index page)</returns>
        [HttpPost]
        public ActionResult Create([Bind(Include = "AccomodationFacilityId,Name,Description,StartHours,EndHours,AccomodationId")] AccomodationFacility accomodationFacility)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;
            accomodationFacility.PhotoUrl = "/Images/defaultAccomodationPhoto.jpg";

            accomodationFacility.AccomodationId = (int)accID;
            if (ModelState.IsValid)
            {
                db.AccomodationFacilities.Add(accomodationFacility);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName", accomodationFacility.AccomodationId);
            return View(accomodationFacility);
        }

        // GET: AccomodationFacilities/Edit/5
        /// <summary>
        /// Edit accommodation FACILITIES (get creation View()) by AccommodationFacility ID
        /// </summary>
        /// <returns>Edit Facilities object to Edit View</returns>
        public ActionResult Edit(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationFacility accomodationFacility = db.AccomodationFacilities.Find(id);
            if (accomodationFacility == null||accomodationFacility.AccomodationId!=accID)
            {
                return HttpNotFound();
            }
            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName", accomodationFacility.AccomodationId);
            return View(accomodationFacility);
        }

        // POST: AccomodationFacilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // GET: AccomodationFacilities/Edit/5
        /// <summary>
        /// Edit accommodation FACILITIES by AccommodationFacility ID and save changes into database
        /// </summary>
        /// <returns>Edit Facilities object to Edit View</returns>
        [HttpPost]
        public ActionResult Edit([Bind(Include = "AccomodationFacilityId,Name,Description,StartHours,EndHours,AccomodationId")] AccomodationFacility accomodationFacility)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;

            accomodationFacility.AccomodationId = (int)accID;

            if (ModelState.IsValid)
            {
                db.Entry(accomodationFacility).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName", accomodationFacility.AccomodationId);
            return View(accomodationFacility);
        }

        /// <summary>
        /// Get Accommodation Facility for Edit
        /// </summary>
        /// <param name="id">Accommodation Facility ID</param>
        /// <returns>Accommodation Facility object for edit</returns>
        public ActionResult EditData(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var room = db.AccomodationFacilities.Where(x => x.AccomodationFacilityId == id).Select(x => new { x.AccomodationFacilityId, x.Description, x.EndHours, x.Name, x.StartHours, x.PhotoUrl, x.AccomodationId}).FirstOrDefault();

            if (room == null||room.AccomodationId!=accID)
            {
                return HttpNotFound();
            }

            var list = JsonConvert.SerializeObject(room,
              Formatting.None,
              new JsonSerializerSettings()
              {
                  ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
              });
            return Content(list, "application/json");
        }

        // GET: AccomodationFacilities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationFacility accomodationFacility = db.AccomodationFacilities.Find(id);
            if (accomodationFacility == null)
            {
                return HttpNotFound();
            }
            return View(accomodationFacility);
        }

        // POST: AccomodationFacilities/Delete/5
        [HttpPost, ActionName("Delete")]
 //       [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccomodationFacility accomodationFacility = db.AccomodationFacilities.Find(id);
            db.AccomodationFacilities.Remove(accomodationFacility);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
