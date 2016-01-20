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

namespace BitBooking.MVC.Controllers
{
   [Authorize(Roles = "Administrator, Hotel Manager")]
    public class AccomodationFacilitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccomodationFacilities
       /// <summary>
       /// Display list of Accomodation Facilities
       /// </summary>
        /// <returns>View of list of Accomodation Facilities</returns>
        public ActionResult Index()
        {

            string userid = User.Identity.GetUserId();
            //int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;




          //  var accomodationFacilities = db.AccomodationFacilities.Include(a => a.Accomodation).Where(a => a.AccomodationId == accID) ;
            return View(db.AccomodationFacilities.ToList());
        }

        // GET: AccomodationFacilities/Details/5
        /// <summary>
        /// Display details of Accomodation Facility
        /// </summary>
        /// <returns>View of Accomodation Facility details</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationFacility accomodationFacility = db.AccomodationFacilities.Find(id);
            string userid = User.Identity.GetUserId();
            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            if (accomodationFacility == null|| accomodationFacility.AccomodationId!=accID)
            {
                return HttpNotFound();
            }
            return View(accomodationFacility);
        }

        // GET: AccomodationFacilities/Create
        /// <summary>
        /// Creates Accomodation Facility
        /// </summary>
        /// <returns>View of Accomodation Facility</returns>
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: AccomodationFacilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccomodationFacilityId,Name,Description,StartHours,EndHours")] AccomodationFacility accomodationFacility)
        {
            string userid = User.Identity.GetUserId();
            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            accomodationFacility.AccomodationId = accID;
            if (ModelState.IsValid)
            {
                db.AccomodationFacilities.Add(accomodationFacility);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

          
            return View(accomodationFacility);
        }

        // GET: AccomodationFacilities/Edit/5
        /// <summary>
        /// Edit certain Accomodation Facility
        /// </summary>
        /// <returns>View of Accomodation Facility</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationFacility accomodationFacility = db.AccomodationFacilities.Find(id);
            string userid = User.Identity.GetUserId();
            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            
            if (accomodationFacility == null||accomodationFacility.AccomodationId!=accID)
            {
                return HttpNotFound();
            }
     
            return View(accomodationFacility);
        }

        // POST: AccomodationFacilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccomodationFacilityId,Name,Description,StartHours,EndHours")] AccomodationFacility accomodationFacility)
        {
            string userid = User.Identity.GetUserId();
            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            accomodationFacility.AccomodationId = accID;
            
            if (ModelState.IsValid)
            {
                db.Entry(accomodationFacility).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
          
            return View(accomodationFacility);
        }

        // GET: AccomodationFacilities/Delete/5
        /// <summary>
        /// Delete certain Accomodation Facility
        /// </summary>
        /// <returns>View of Accomodation Facility </returns>
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
        /// <summary>
        /// Confirming delete of certain Accomodation Facility
        /// </summary>
        /// <returns>View of Index page </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
