using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitBooking.DAL.Models;

namespace BitBooking.MVC.Controllers
{
   [Authorize(Roles = "Administrator, Hotel Manager")]
    public class AccomodationServiceTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccomodationServiceTypes
        /// <summary>
        /// Display list of Accomodations Services Types
        /// </summary>
        /// <returns>View list of Accomodations Services Types</returns>
        public ActionResult Index()
        {
            return View(db.AccomodationServiceTypes.ToList());
        }

        // GET: AccomodationServiceTypes/Details/5
        /// <summary>
        /// Display details of Accomodations Services Types
        /// </summary>
        /// <returns>View of Accomodations Services Types</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationServiceType accomodationServiceType = db.AccomodationServiceTypes.Find(id);
            if (accomodationServiceType == null)
            {
                return HttpNotFound();
            }
            return View(accomodationServiceType);
        }

        // GET: AccomodationServiceTypes/Create
        /// <summary>
        /// Create Accomodations Services Types
        /// </summary>
        /// <returns>View of Accomodations Services Types</returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccomodationServiceTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccomodationServiceTypeId,Name")] AccomodationServiceType accomodationServiceType)
        {
            if (ModelState.IsValid)
            {
                db.AccomodationServiceTypes.Add(accomodationServiceType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accomodationServiceType);
        }

        // GET: AccomodationServiceTypes/Edit/5
        /// <summary>
        /// Edit Accomodations Services Types
        /// </summary>
        /// <returns>View of Accomodations Services Types</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationServiceType accomodationServiceType = db.AccomodationServiceTypes.Find(id);
            if (accomodationServiceType == null)
            {
                return HttpNotFound();
            }
            return View(accomodationServiceType);
        }

        // POST: AccomodationServiceTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccomodationServiceTypeId,Name")] AccomodationServiceType accomodationServiceType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accomodationServiceType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accomodationServiceType);
        }

        // GET: AccomodationServiceTypes/Delete/5
        /// <summary>
        /// Delete Accomodations Services Types
        /// </summary>
        /// <returns>View of Accomodations Services Types</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationServiceType accomodationServiceType = db.AccomodationServiceTypes.Find(id);
            if (accomodationServiceType == null)
            {
                return HttpNotFound();
            }
            return View(accomodationServiceType);
        }

        // POST: AccomodationServiceTypes/Delete/5
        /// <summary>
        /// Confirm Delete of Accomodations Services Types
        /// </summary>
        /// <returns>View of Index page</returns>
       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccomodationServiceType accomodationServiceType = db.AccomodationServiceTypes.Find(id);
            db.AccomodationServiceTypes.Remove(accomodationServiceType);
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
