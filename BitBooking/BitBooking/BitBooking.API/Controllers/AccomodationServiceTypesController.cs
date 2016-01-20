using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitBooking.DAL.Models;

namespace BitBooking.API.Controllers
{
    public class AccomodationServiceTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccomodationServiceTypes
        public ActionResult Index()
        {
            return View(db.AccomodationServiceTypes.ToList());
        }

        // GET: AccomodationServiceTypes/Details/5
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
