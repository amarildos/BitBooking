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
    public class RoomPricesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
      [Authorize(Roles = "Administrator, Hotel Manager")]
        // GET: RoomPrices
        /// <summary>
        /// Display list of Room Prices
        /// </summary>
        /// <returns>View list of Room Prices</returns>
        public ActionResult Index()
        {
            

            string userid = User.Identity.GetUserId();

            if(userid==null)
            {

                return View("Notfound");
            }
         //   int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
          //  var roomPrices = db.RoomPrices.Include(r => r.Accomodation).Include(r => r.RoomType).Where(r=>r.AccomodationId==accID);
            return View(db.RoomPrices.ToList());
        }

        // GET: RoomPrices/Details/5
       /// <summary>
       /// Display details Room Prices
       /// </summary>
       /// <returns>View Room Prices</returns>
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
        /// <summary>
        /// Create Room Prices
        /// </summary>
        /// <returns>View Room Prices</returns>
        public ActionResult Create()
        {
            string userid = User.Identity.GetUserId();
            if (userid == null)
            {

                return View("Notfound");
            }
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName");
            return View();
        }

        // POST: RoomPrices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoomPriceId,RoomTypeId,SpecialPrice,StartDate,EndDate")] RoomPrice roomPrice)
        {

            string userid = User.Identity.GetUserId();
          
            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            roomPrice.AccomodationId = accID;
            if (ModelState.IsValid)
            {
                db.RoomPrices.Add(roomPrice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName", roomPrice.RoomTypeId);
            return View(roomPrice);
        }

        // GET: RoomPrices/Edit/5
        /// <summary>
        /// Edit Room Prices
        /// </summary>
        /// <returns>View Room Prices</returns>
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
            
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName", roomPrice.RoomTypeId);
            return View(roomPrice);
        }

        // POST: RoomPrices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoomPriceId,RoomTypeId,SpecialPrice,StartDate,EndDate")] RoomPrice roomPrice)
        {
            string userid = User.Identity.GetUserId();
            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            
            roomPrice.AccomodationId = accID;
            if (ModelState.IsValid)
            {
                db.Entry(roomPrice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
          
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName", roomPrice.RoomTypeId);
            return View(roomPrice);
        }

        // GET: RoomPrices/Delete/5
        /// <summary>
        /// Delete Room Prices
        /// </summary>
        /// <returns>View Room Prices</returns>
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
        /// <summary>
        /// Confirm delete of Room Prices
        /// </summary>
        /// <returns>View of Index page</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
