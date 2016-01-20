using BitBooking.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Net;

namespace BitBooking.MVC.Controllers
{
    public class LastStepController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            ViewBag.AccomodationServiceTypeId = new SelectList(db.AccomodationServiceTypes, "AccomodationServiceTypeId", "Name");

            return View();
        }

        
        public ActionResult AddServices(FormCollection collection)
        {
            string userid = User.Identity.GetUserId();

            if (userid == null)
            {

                return View("Notfound");
            }

            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            var one = collection[0];
            var two = collection[1];
            var three = "";
            try
            {
                 three = collection[2] + "," + collection[3];
            }

            catch
            {
                 three = collection[2];

            }
            string[] array1 =collection[1].Split(',');
            string[] array2 =three.Split(',');

            if (array1.Length == array2.Length)
            {
                for (int i = 0; i < array1.Length; i++)
                {

                    AccomodationService service = new AccomodationService { AccomodationServiceTypeId = Convert.ToInt32(array2[i]), Name = array1[i], AccomodationId = accID };
                    if (service.AccomodationServiceTypeId != 0 && service.Name != "")
                    {
                        db.AccomodationServices.Add(service);
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("UpdateInfo");
            }

            else
            {

                return View("Index");

            }
        }


        public ActionResult Updategoogle(string string1, string string2)
        {

            string userid = User.Identity.GetUserId();

            if (userid == null)
            {

                return View("Notfound");
            }

            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;

            AccomodationInfo info = db.AccomodationInfoes.FirstOrDefault(x => x.AccomodationId == accID);
            info.GoogleX = string1;
            info.GoogleY = string2;
            db.Entry(info).State = EntityState.Modified;
            db.SaveChanges();
            
            return Json("OK");

        }

        // GET: LastStep/Create
        public ActionResult Create()
        {
            ViewBag.AccomodationServiceTypeId1 = new SelectList(db.AccomodationServiceTypes, "AccomodationServiceTypeId", "Name");
            return View();
        }

        public ActionResult Rooms()
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
        


            for (int i = 0; i < rums.Count; i++ )
            {

                rooms.RemoveAll(x => x.NumberOfRooms == rums[i].NumberOfRooms&& x.RoomCapacity==rums[i].RoomCapacity && x.RoomType == rums[i].RoomType && x.Price == rums[i].Price && x.RoomId != rums[i].RoomId);



            }

             return View(rooms);
            }
         
          

     
        // POST: LastStep/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LastStep/Edit/5
        public ActionResult UpdateInfo()
        {

            string userid = User.Identity.GetUserId();

            if (userid == null)
            {

                return View("Notfound");
            }
            var  accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            var acomodat = db.Accomodations.Where(x => x.AccomodationId == accID);
            var info = db.AccomodationInfoes.Where(x => x.AccomodationId ==accID).FirstOrDefault();


            return View(info);
        }

        // POST: LastStep/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult UpdateInfo([Bind(Include = "AccomodationInfoId,Address,City,PostalCode,Country,Phone,Email,GoogleX, GoogleY")] AccomodationInfo accomodationInfo)
        {string userid = User.Identity.GetUserId();
            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
            accomodationInfo.AccomodationId = accID;
         //must fix this
           // string first = db.Accomodations.Find(accID).AccomodationInfo.GoogleX;
         //   string second = db.Accomodations.Find(accID).AccomodationInfo.GoogleY;
            //accomodationInfo.GoogleX = "1";
            //accomodationInfo.GoogleY = second;
            if (ModelState.IsValid)
            {
                db.Entry(accomodationInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Rooms");
            }
            return View(accomodationInfo);
        }


        // GET: LastStep/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }


        
        public ActionResult AccomodationPhotos()
        {
            Photo test = new Photo { RoomTypeId=1, Priority = 2 };
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName");
            //return View(test);
            return View(test);

        }
        // POST: LastStep/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
