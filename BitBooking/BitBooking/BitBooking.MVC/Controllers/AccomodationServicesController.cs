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
    public class AccomodationServicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
         [Authorize(Roles = "Administrator, Hotel Manager")]
        // GET: AccomodationServices
        /// <summary>
        /// Display list of Accomodations Services
        /// </summary>
        /// <returns>View list of Accomodations Services</returns>
        public ActionResult Index()
        {
            string userid = User.Identity.GetUserId();
             if(userid==null)
             {

                 return View("Notfound");
             }
        //    int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
       
          //  var accomodationServices = db.AccomodationServices.Include(a => a.AccomodationServiceType).Where(x => x.AccomodationId == accID) ;
            return View(db.AccomodationServices.ToList());
        }

        // GET: AccomodationServices/Details/5
         /// <summary>
         /// Display details of Accomodations Services
         /// </summary>
         /// <returns>View of Accomodations Services</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationService accomodationService = db.AccomodationServices.Find(id);
          //  string userid = User.Identity.GetUserId();
        //    int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            if (accomodationService == null)
            {
                return HttpNotFound();
            }
            return View(accomodationService);
        }

        // GET: AccomodationServices/Create
        /// <summary>
        /// Create Accomodations Services
        /// </summary>
        /// <returns>View of Accomodations Services</returns>
        public ActionResult Create()
        {
            ViewBag.AccomodationServiceTypeId = new SelectList(db.AccomodationServiceTypes, "AccomodationServiceTypeId", "Name");

            return View();
        }

        // POST: AccomodationServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccomodationServiceId,Name,AccomodationServiceTypeId")] AccomodationService accomodationService)
        {
            string userid = User.Identity.GetUserId();
            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            accomodationService.AccomodationId = accID;
            if (ModelState.IsValid)
            {
                db.AccomodationServices.Add(accomodationService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccomodationServiceTypeId = new SelectList(db.AccomodationServiceTypes, "AccomodationServiceTypeId", "Name");

            return View(accomodationService);
        }

        // GET: AccomodationServices/Edit/5
        /// <summary>
        /// Edit Accomodations Services
        /// </summary>
        /// <returns>View of Accomodations Services</returns>
        public ActionResult Edit(int? id)
        {
            ViewBag.AccomodationServiceTypeId = new SelectList(db.AccomodationServiceTypes, "AccomodationServiceTypeId", "Name");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationService accomodationService = db.AccomodationServices.Find(id);
            string userid = User.Identity.GetUserId();
            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            if (accomodationService == null||accomodationService.AccomodationId!=accID)
            {
                return HttpNotFound();
            }
            return View(accomodationService);
        }

        // POST: AccomodationServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccomodationServiceId,Name,AccomodationServiceTypeId")] AccomodationService accomodationService)
        {
            ViewBag.AccomodationServiceTypeId = new SelectList(db.AccomodationServiceTypes, "AccomodationServiceTypeId", "Name");

            string userid = User.Identity.GetUserId();
            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            accomodationService.AccomodationId = accID;
            if (ModelState.IsValid)
            {
                db.Entry(accomodationService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accomodationService);
        }

        // GET: AccomodationServices/Delete/5
        /// <summary>
        /// Delete Accomodations Services
        /// </summary>
        /// <returns>View of Accomodations Services</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationService accomodationService = db.AccomodationServices.Find(id);
            if (accomodationService == null)
            {
                return HttpNotFound();
            }
            return View(accomodationService);
        }

        // POST: AccomodationServices/Delete/5
        /// <summary>
        /// Confirm delete of Accomodations Services
        /// </summary>
        /// <returns>View of Index</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccomodationService accomodationService = db.AccomodationServices.Find(id);
            db.AccomodationServices.Remove(accomodationService);
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
