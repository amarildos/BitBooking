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
    public class AccomodationInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccomodationInfoes
        /// <summary>
        /// Display list of Accomodation Info
        /// </summary>
        /// <returns>View list of Accomodation Info</returns>
        public ActionResult Index()
        {
          
            
            return View(db.AccomodationInfoes.ToList());
        }

        // GET: AccomodationInfoes/Details/5
        /// <summary>
        /// Display details of Accomodation Info
        /// </summary>
        /// <returns>View details of Accomodation Info</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationInfo accomodationInfo = db.AccomodationInfoes.Find(id);
            string userid = User.Identity.GetUserId();
            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;

          
            return View(accomodationInfo);
        }

        // GET: AccomodationInfoes/Create
        /// <summary>
        /// Create Accomodation Info
        /// </summary>
        /// <returns>View Accomodation Info</returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccomodationInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccomodationInfoId,Address,City,PostalCode,Country,Phone,Email")] AccomodationInfo accomodationInfo)
        {
            string userid = User.Identity.GetUserId();
            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            accomodationInfo.AccomodationId = accID;
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
            string userid = User.Identity.GetUserId();
            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            if (accomodationInfo == null||accomodationInfo.AccomodationId!=accID)
            {
                return HttpNotFound();
            }
            return View(accomodationInfo);
        }

        // POST: AccomodationInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccomodationInfoId,Address,City,PostalCode,Country,Phone,Email")] AccomodationInfo accomodationInfo)
        {
            string userid = User.Identity.GetUserId();
            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            accomodationInfo.AccomodationId = accID;
            if (ModelState.IsValid)
            {
                db.Entry(accomodationInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accomodationInfo);
        }
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

        // GET: AccomodationInfoes/Delete/5
        /// <summary>
        /// Delete Accomodation Info
        /// </summary>
        /// <returns>View Accomodation Info</returns>
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
        /// <summary>
        /// Confirm delete of Accomodation Info
        /// </summary>
        /// <returns>View of Index page</returns>
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
