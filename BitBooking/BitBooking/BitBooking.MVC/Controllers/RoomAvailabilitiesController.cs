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
    public class RoomAvailabilitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RoomAvailabilities
        public ActionResult Index()
        {
            string userid = User.Identity.GetUserId();

            if (userid == null)
            {

                return View("Notfound");
            }
            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            return View(db.RoomAvailabilities.ToList());
        }

        // GET: RoomAvailabilities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomAvailability roomAvailability = db.RoomAvailabilities.Find(id);
            if (roomAvailability == null)
            {
                return HttpNotFound();
            }
            return View(roomAvailability);
        }

        // GET: RoomAvailabilities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomAvailabilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoomAvailabilityId,RoomId,AccomodationId,ArrivalDate,DepartureDate")] RoomAvailability roomAvailability)
        {
            if (ModelState.IsValid)
            {
                db.RoomAvailabilities.Add(roomAvailability);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roomAvailability);
        }

        // GET: RoomAvailabilities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomAvailability roomAvailability = db.RoomAvailabilities.Find(id);
            if (roomAvailability == null)
            {
                return HttpNotFound();
            }
            return View(roomAvailability);
        }

        // POST: RoomAvailabilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoomAvailabilityId,RoomId,AccomodationId,ArrivalDate,DepartureDate")] RoomAvailability roomAvailability)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomAvailability).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roomAvailability);
        }

        // GET: RoomAvailabilities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomAvailability roomAvailability = db.RoomAvailabilities.Find(id);
            if (roomAvailability == null)
            {
                return HttpNotFound();
            }
            return View(roomAvailability);
        }

        // POST: RoomAvailabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomAvailability roomAvailability = db.RoomAvailabilities.Find(id);
            db.RoomAvailabilities.Remove(roomAvailability);
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
