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
    public class RoomTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RoomTypes
        /// <summary>
        /// Display list of Room Types
        /// </summary>
        /// <returns>View list of Room Types</returns>
        public ActionResult Index()
        {
            return View(db.RoomTypes.ToList());
        }

        // GET: RoomTypes/Details/5
        /// <summary>
        /// Display details of Room Types
        /// </summary>
        /// <returns>View of Room Types</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomType roomType = db.RoomTypes.Find(id);
            if (roomType == null)
            {
                return HttpNotFound();
            }
            return View(roomType);
        }

        // GET: RoomTypes/Create
        /// <summary>
        /// Create Room Types
        /// </summary>
        /// <returns>View of Room Types</returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoomTypeId,RoomTypeName")] RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                db.RoomTypes.Add(roomType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roomType);
        }

        // GET: RoomTypes/Edit/5
        /// <summary>
        /// Edit Room Types
        /// </summary>
        /// <returns>View of Room Types</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomType roomType = db.RoomTypes.Find(id);
            if (roomType == null)
            {
                return HttpNotFound();
            }
            return View(roomType);
        }

        // POST: RoomTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoomTypeId,RoomTypeName")] RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roomType);
        }

        // GET: RoomTypes/Delete/5
        /// <summary>
        /// Delete  Room Types
        /// </summary>
        /// <returns>View Room Types</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomType roomType = db.RoomTypes.Find(id);
            if (roomType == null)
            {
                return HttpNotFound();
            }
            return View(roomType);
        }

        // POST: RoomTypes/Delete/5
        /// <summary>
        /// Confirm delete of Room Types
        /// </summary>
        /// <returns>View of Index page</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomType roomType = db.RoomTypes.Find(id);
            db.RoomTypes.Remove(roomType);
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
