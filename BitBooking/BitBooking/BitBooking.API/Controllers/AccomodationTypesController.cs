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
    public class AccomodationTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccomodationTypes
        public ActionResult Index()
        {
            return View(db.AccomodationTypes.ToList());
        }

        // GET: AccomodationTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationType accomodationType = db.AccomodationTypes.Find(id);
            if (accomodationType == null)
            {
                return HttpNotFound();
            }
            return View(accomodationType);
        }

        // GET: AccomodationTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccomodationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccomodationTypeId,AccomodationTypeName")] AccomodationType accomodationType)
        {
            if (ModelState.IsValid)
            {
                db.AccomodationTypes.Add(accomodationType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accomodationType);
        }

        // GET: AccomodationTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationType accomodationType = db.AccomodationTypes.Find(id);
            if (accomodationType == null)
            {
                return HttpNotFound();
            }
            return View(accomodationType);
        }

        // POST: AccomodationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccomodationTypeId,AccomodationTypeName")] AccomodationType accomodationType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accomodationType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accomodationType);
        }

        // GET: AccomodationTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationType accomodationType = db.AccomodationTypes.Find(id);
            if (accomodationType == null)
            {
                return HttpNotFound();
            }
            return View(accomodationType);
        }

        // POST: AccomodationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccomodationType accomodationType = db.AccomodationTypes.Find(id);
            db.AccomodationTypes.Remove(accomodationType);
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
