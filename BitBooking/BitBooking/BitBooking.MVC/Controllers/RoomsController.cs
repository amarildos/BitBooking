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
    public class RoomsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
         [Authorize(Roles = "Administrator, Hotel Manager")]
        // GET: Rooms
        public ActionResult Index()
        {
            string userid = User.Identity.GetUserId();

            if (userid == null)
            {

                return View("Notfound");
            }
          
          //  int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            
            //var rooms = db.Rooms.Include(r => r.Accomodation).Include(r => r.RoomType).Where(r=>r.AccomodationId==accID);


            return View(db.Rooms.ToList());
        }
         public ActionResult Specific()
         {
             string userid = User.Identity.GetUserId();

             if (userid == null)
             {

                 return View("Notfound");
             }

             int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;

           
             List<Room> rums = new List<Room>();
             var rooms = db.Rooms.Include(r => r.Accomodation).Include(r => r.RoomType).Where(r => r.AccomodationId == accID).ToList();
             rums = rooms;



             for (int i = 0; i < rums.Count; i++)
             {

                 rooms.RemoveAll(x => x.NumberOfRooms == rums[i].NumberOfRooms && x.RoomType == rums[i].RoomType && x.Price == rums[i].Price && x.RoomId != rums[i].RoomId);



             }

             return View(rooms);
         }
         public ActionResult GetRooms()
         {
             string userid = User.Identity.GetUserId();

             if (userid == null)
             {

                 return View("Notfound");
             }

             int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;

             var rooms = db.Rooms.Include(r => r.Accomodation).Include(r => r.RoomType).Where(r => r.AccomodationId == accID).ToArray();
             return Json(rooms);
         }



         [Authorize(Roles = "Administrator, Hotel Manager")]
        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            string juzer = User.Identity.GetUserId();
            int accID = db.Users.FirstOrDefault(x => x.Id == juzer).AccomodationId;
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }
        [Authorize(Roles="Administrator, Seller")]
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
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoomId,NumberOfRooms,Price,RoomCapacity,RoomTypeId")] Room room)
        {

            string userid = User.Identity.GetUserId();
            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
           // room.AccomodationId = accID;
            if (ModelState.IsValid)
            {
                for (int i = 0; i < room.NumberOfRooms; i++ )
                {
                    db.Rooms.Add(room);
                    db.SaveChanges();
                }
                 
              //  return RedirectToAction("Index");
                return Json(room);
            }

        
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName", room.RoomTypeId);
            return View(room);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2([Bind(Include = "RoomId,NumberOfRooms,Price,RoomCapacity,RoomTypeId")] Room room)
        {

            string userid = User.Identity.GetUserId();
            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            room.AccomodationId = accID;
            if (ModelState.IsValid)
            {
                for (int i = 0; i < room.NumberOfRooms; i++)
                {
                    db.Rooms.Add(room);
                    db.SaveChanges();
                }

                //  return RedirectToAction("Index");
                return RedirectToAction("Create");
            }


            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName", room.RoomTypeId);
            return View("Create", room);
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
          
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName", room.RoomTypeId);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoomId,NumberOfRooms,Price,RoomCapacity,RoomTypeId")] Room room)
        {
            string userid = User.Identity.GetUserId();
            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            room.AccomodationId = accID;
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
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
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
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
