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
    public class RoomPricesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RoomPrices
        public ActionResult Index()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;

            var roomPrices = db.RoomPrices.Include(x => x.Accomodation).Include(x => x.RoomType).Where(x=>x.AccomodationId==accID);

            return Json(roomPrices, JsonRequestBehavior.AllowGet);
        }

        // GET: RoomPrices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomPrice roomPrice = db.RoomPrices.Find(id);
            if (roomPrice == null)
            {
                return HttpNotFound();
            }
            return View(roomPrice);
        }

        // GET: RoomPrices/Create
        public ActionResult Create()
        {
            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName");
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName");
            return View();
        }

        // POST: RoomPrices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "RoomPriceId,RoomTypeId,AccomodationId,SpecialPrice,StartDate,EndDate")] RoomPrice roomPrice)
        {db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;

            roomPrice.AccomodationId = (int)accID;

            if (ModelState.IsValid)
            {
                db.RoomPrices.Add(roomPrice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName", roomPrice.AccomodationId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName", roomPrice.RoomTypeId);
            return View(roomPrice);
        }

        // GET: RoomPrices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomPrice roomPrice = db.RoomPrices.Find(id);
            if (roomPrice == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName", roomPrice.AccomodationId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName", roomPrice.RoomTypeId);
            return View(roomPrice);
        }

        // POST: RoomPrices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoomPriceId,RoomTypeId,AccomodationId,SpecialPrice,StartDate,EndDate")] RoomPrice roomPrice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomPrice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName", roomPrice.AccomodationId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName", roomPrice.RoomTypeId);
            return View(roomPrice);
        }

        // GET: RoomPrices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomPrice roomPrice = db.RoomPrices.Find(id);
            if (roomPrice == null)
            {
                return HttpNotFound();
            }
            return View(roomPrice);
        }

        // POST: RoomPrices/Delete/5
        [HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomPrice roomPrice = db.RoomPrices.Find(id);
            db.RoomPrices.Remove(roomPrice);
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
