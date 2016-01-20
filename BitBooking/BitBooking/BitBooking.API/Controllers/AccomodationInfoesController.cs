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
namespace BitBooking.API.Controllers
{
    public class AccomodationInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccomodationInfoes
        /// <summary>
        /// Get list of Accommodation Info's (all accommodation addresses) for certain seller (accommodation representative)
        /// </summary>
        /// <returns>List of AccommodationInfo objects</returns>
        public ActionResult Index()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;


            return Json(db.AccomodationInfoes.Where(x=>x.AccomodationId==accID).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        // GET: AccomodationInfoes/Details/5
        /// <summary>
        /// Get AccommodationInfo details by AccomodationInfo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AccomodationInfo Object</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationInfo accomodationInfo = db.AccomodationInfoes.Find(id);
            if (accomodationInfo == null)
            {
                return HttpNotFound();
            }
            return View(accomodationInfo);
        }


        /// <summary>
        /// Get AccommodationInfo object
        /// </summary>
        /// <param name="id">AccommodationInfo ID</param>
        /// <returns>AccommodationInfo object</returns>
        [HttpPost]
        public ActionResult GetCoordinates(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var info = db.AccomodationInfoes.FirstOrDefault(x => x.AccomodationId == id);
            return Json(info);
        }

        // GET: AccomodationInfoes/Create
        /// <summary>
        /// Get View for Creating AccommodationInfo Objects
        /// </summary>
        /// <returns>View - Html for creating AccommodationInfo object</returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccomodationInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Creates AccommodationInfo object with 
        /// </summary>
        /// <param name="accomodationInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccomodationInfoId,Address,City,PostalCode,Country,Phone,Email,AccomodationId")] AccomodationInfo accomodationInfo)
        {
            if (ModelState.IsValid)
            {
                db.AccomodationInfoes.Add(accomodationInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accomodationInfo);
        }

        // GET: AccomodationInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationInfo accomodationInfo = db.AccomodationInfoes.Find(id);
            if (accomodationInfo == null)
            {
                return HttpNotFound();
            }
            return View(accomodationInfo);
        }

        // POST: AccomodationInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
  
        public ActionResult Edit([Bind(Include = "AccomodationInfoId,Address,City,PostalCode,Country,Phone,Email,AccomodationId,GoogleX, GoogleY")] AccomodationInfo accomodationInfo)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (ModelState.IsValid)
            {
                db.Entry(accomodationInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accomodationInfo);
        }
        [HttpPost]

        public ActionResult UpdateValues(string x, string y)
        {
            x = x.Replace(',', '.');
            y = y.Replace(',', '.');
            db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(dx => dx.Id == userId).AccomodationId;
            AccomodationInfo accomodationInfo = db.AccomodationInfoes.FirstOrDefault(yy => yy.AccomodationId == accID);
            accomodationInfo.GoogleX = x;
            accomodationInfo.GoogleY = y;
            if (ModelState.IsValid)
            {
                db.Entry(accomodationInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accomodationInfo);
        }
        // GET: AccomodationInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationInfo accomodationInfo = db.AccomodationInfoes.Find(id);
            if (accomodationInfo == null)
            {
                return HttpNotFound();
            }
            return View(accomodationInfo);
        }

        // POST: AccomodationInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccomodationInfo accomodationInfo = db.AccomodationInfoes.Find(id);
            db.AccomodationInfoes.Remove(accomodationInfo);
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
