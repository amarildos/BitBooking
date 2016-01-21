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
    public class RoomsController : Controller
    {
        public RoomsController()
        {

            db.Configuration.ProxyCreationEnabled = false;
        }
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rooms
        public ActionResult Index()
        {
            try
            {
                string userid = User.Identity.GetUserId();

                int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;

           
            List<Room> rooms_temp = new List<Room>();

            rooms_temp = db.Rooms.Include(x => x.RoomType).Where(x => x.AccomodationId==accID).ToList();

            List<Photo> photoList = new List<Photo>();

            photoList = db.Images.Where(x => x.AccomodationId == accID).ToList();

            List<Photo> newPhotoList = new List<Photo>();
         

            foreach(var photo in photoList)
            {
                var photoX = new Photo
                {
                    AccomodationId = photo.AccomodationId,
                    FacilityId = photo.FacilityId,
                    PhotoId = photo.PhotoId,
                    PhotoUrl = photo.PhotoUrl,
                    Priority = photo.Priority,
                    RoomTypeId = photo.RoomTypeId
                };
                newPhotoList.Add(photoX);
            }


            foreach(var room in rooms_temp)
            {
                room.RoomType.ListOfPhotos = newPhotoList.Where(x => x.RoomTypeId == room.RoomTypeId).ToList();
            }


            return Json(rooms_temp.ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                //throw new UnauthorizedAccessException(e.Message);
                return Json(null);
            };
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName");
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      //  [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "RoomId,NumberOfRooms,Price,RoomCapacity,RoomTypeId,AccomodationId, RoomDetails")] Room room)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;

            room.AccomodationId = (int)accID;

            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName", room.AccomodationId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes.OrderBy(x=>x.RoomTypeName), "RoomTypeId", "RoomTypeName", room.RoomTypeId);
            return View(room);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName", room.AccomodationId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName", room.RoomTypeId);
            return View(room);
        }


        public ActionResult EditData(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Include(x=>x.RoomType).Where(x=>x.RoomId==id).FirstOrDefault();
            
            if (room == null)
            {
                return HttpNotFound();
            }
            return Json(room, JsonRequestBehavior.AllowGet);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Edit([Bind(Include = "RoomId,NumberOfRooms,Price,RoomCapacity,RoomTypeId,AccomodationId, RoomDetails")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName", room.AccomodationId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName", room.RoomTypeId);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
  
        public ActionResult DeleteConfirmed(int id)
        {

            db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;



            Room room = db.Rooms.Find(id);
            if (room.AccomodationId != accID)
            { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }

            db.Rooms.Remove(room);
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
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
