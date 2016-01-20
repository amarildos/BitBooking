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
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace BitBooking.API.Controllers
{
    public class RoomAvailabilitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
                // GET: RoomAvailabilities
        public ActionResult Index()
        {
            
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;
            if (accID != 0)
            {
                List<RoomAvailability> listOfUnpaidRooms = new List<RoomAvailability>();
                List<BitBooking.API.Models.ReservationsPaymentAPI> listOfUnpaidRoomsFullDetails = new List<BitBooking.API.Models.ReservationsPaymentAPI>();

                listOfUnpaidRooms = db.RoomAvailabilities.Where(x => x.AccomodationId == accID).ToList();
                
                if (listOfUnpaidRooms.Count > 0)
                {
                    foreach (var room in listOfUnpaidRooms)
                    {
                        string accomodationName = db.Accomodations.Single(x => x.AccomodationId == room.AccomodationId).AccomodationName;
                        string roomName = db.Rooms.Single(x => x.RoomId == room.RoomId).RoomType.RoomTypeName;

                        listOfUnpaidRoomsFullDetails.Add(new BitBooking.API.Models.ReservationsPaymentAPI
                        {
                            AccommodationName = accomodationName,
                            RoomName = roomName,
                            AccomodationId = room.AccomodationId,
                            ArrivalDate = room.ArrivalDate,
                            DepartureDate = room.DepartureDate,
                            IsPaid = room.IsPaid,
                            RoomAvailabilityId = room.RoomAvailabilityId,
                            RoomId = room.RoomId,
                            TotalPrice = room.TotalPrice,
                            UserId = room.UserId,
                            UserEmail = room.UserEmail
                        });
                    }
                }
                return Json(listOfUnpaidRoomsFullDetails, JsonRequestBehavior.AllowGet);
            }
            if(userId!=null)
            {
                List<RoomAvailability> listOfUnpaidRooms = new List<RoomAvailability>();
                List<BitBooking.API.Models.ReservationsPaymentAPI> listOfUnpaidRoomsFullDetails = new List<BitBooking.API.Models.ReservationsPaymentAPI>();

                listOfUnpaidRooms = db.RoomAvailabilities.Where(x => x.UserId == userId).ToList();

                if (listOfUnpaidRooms.Count > 0)
                {
                    foreach (var room in listOfUnpaidRooms)
                    {
                        string accomodationName = db.Accomodations.Single(x => x.AccomodationId == room.AccomodationId).AccomodationName;
                        string roomName = db.Rooms.Single(x => x.RoomId == room.RoomId).RoomType.RoomTypeName;

                        listOfUnpaidRoomsFullDetails.Add(new BitBooking.API.Models.ReservationsPaymentAPI
                        {
                            AccommodationName = accomodationName,
                            RoomName = roomName,
                            AccomodationId = room.AccomodationId,
                            ArrivalDate = room.ArrivalDate,
                            DepartureDate = room.DepartureDate,
                            IsPaid = room.IsPaid,
                            RoomAvailabilityId = room.RoomAvailabilityId,
                            RoomId = room.RoomId,
                            TotalPrice = room.TotalPrice,
                            UserId = room.UserId,
                            UserEmail = room.UserEmail
                        });
                    }
                }

                return Json(listOfUnpaidRoomsFullDetails, JsonRequestBehavior.AllowGet);
            }
            return Json(db.RoomAvailabilities.ToList(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult UserReservations()
        {
            var userId = this.User.Identity.GetUserId();
            List<RoomAvailability> userreservations = db.RoomAvailabilities.Where(x => x.UserId == userId).ToList();
            return View(userreservations);
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
        public ActionResult Create([Bind(Include = "RoomAvailabilityId,RoomId,AccomodationId,ArrivalDate,DepartureDate,UserId,IsPaid,TotalPrice")] RoomAvailability roomAvailability)
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
        public ActionResult Edit([Bind(Include = "RoomAvailabilityId,RoomId,AccomodationId,ArrivalDate,DepartureDate,UserId,IsPaid,TotalPrice")] RoomAvailability roomAvailability)
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


        public ActionResult GetAvailability()
        {
            return View();
        }

        // GET: RoomAvailabilities/GetAvailability
        [HttpPost]
        //public ActionResult GetAvailability(DateTime? from, DateTime? to)
        //{

        //    //TESTNI PODACI SU U PITANJU - TREBA IMPLEMENTIRATI PRAVE PODATKE 
        //    //TESTNI PODACI SU U PITANJU - TREBA IMPLEMENTIRATI PRAVE PODATKE 
        //    //TESTNI PODACI SU U PITANJU - TREBA IMPLEMENTIRATI PRAVE PODATKE 
        //    //TESTNI PODACI SU U PITANJU - TREBA IMPLEMENTIRATI PRAVE PODATKE
        //    //TESTNI PODACI SU U PITANJU - TREBA IMPLEMENTIRATI PRAVE PODATKE 

        //    RoomAvailability roomCheck = new RoomAvailability() { AccomodationId = 50, ArrivalDate = from.Value, DepartureDate = to.Value, IsPaid = true, RoomId = 67, RoomAvailabilityId = 3, UserId = 1 };

        //    var currentBooking = db.RoomAvailabilities.Where((x => x.RoomId == 67 && x.AccomodationId == 50 &&
        //        (from <= x.DepartureDate && from >= x.ArrivalDate || x.ArrivalDate <= to && x.ArrivalDate >= from)))
        //        .FirstOrDefault();

        //    if (currentBooking != null)
        //    {
        //        return View(roomCheck);
        //    }

        //    db.RoomAvailabilities.Add(roomCheck);
        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        public Room GetAvailabilityAndPrice(int accomodationId, int roomId, DateTime? from, DateTime? to,string UserId)   //nedostaje UserID da bi se znalo za koga se soba rezerviše
        {
            var currentBooking = db.RoomAvailabilities.Where((x => x.RoomId == roomId && x.AccomodationId == accomodationId &&
                (from <= x.DepartureDate && from >= x.ArrivalDate || x.ArrivalDate <= to && x.ArrivalDate >= from)))
                .ToList();

            //if (currentBooking != null)
            //{
            //    return null;
            //}

            
            var roomTypeId = db.Rooms.Where(x => x.RoomId == roomId).SingleOrDefault().RoomTypeId;
            var pricePerNight = db.Rooms.Where(x => x.RoomId == roomId).SingleOrDefault().Price;
            var numberOfRooms = db.Rooms.Where(x => x.RoomId == roomId).SingleOrDefault().NumberOfRooms;
            var roomCapacity = db.Rooms.Where(x => x.RoomId == roomId).SingleOrDefault().RoomCapacity;

            //START - NEW TEST

            if (currentBooking.Count >= numberOfRooms)
            {
                return null;
            }
            
            //END - NEW TEST



            Room foundRoom = new Room() { 
                AccomodationId = accomodationId, 
                Price = pricePerNight, 
                RoomId = roomId, 
                RoomTypeId = roomTypeId,
                ListOfAvailableRooms = null,
                NumberOfRooms = numberOfRooms,
                RoomCapacity = roomCapacity,
                TempArrivalDate = from.Value,
                TempDepartureDate = to.Value,
                TempIsPaid = false,
                TempUserId = UserId
            };
           

            double totalPrice = 0;
            DateTime currentDay = new DateTime();
            double pricePerDay = 0;

            currentDay = from.Value;

            while (currentDay <= to.Value)
            {
                try
                {
                    pricePerDay = db.RoomPrices.Where(x => x.StartDate <= currentDay && x.EndDate >= currentDay && x.RoomTypeId == roomTypeId && x.AccomodationId == foundRoom.AccomodationId)
                                        .FirstOrDefault().SpecialPrice;
                }
                catch (Exception)
                {
                    //Implement the error LOG when the user does not sets the prices for a room on certain dates. The error will be cought here!
                    totalPrice += db.Rooms.Where(x => x.RoomId == roomId).SingleOrDefault().Price;
                }
                totalPrice += pricePerDay;
                currentDay = currentDay.AddDays(1);
            }

            double totalDays = (to.Value - from.Value).TotalDays;

            foundRoom.Price = totalPrice / totalDays;

            foundRoom.TempTotalPrice = totalPrice;
            
            return foundRoom;
        }

        // GET: RoomAvailabilities/ReserveRoom/5
        public ActionResult ReserveRoom(int? id, DateTime ArrivalDate, DateTime DepartureDate, bool IsPaid, double TotalPrice, string UserId,double Price)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room roomToReserve = db.Rooms.SingleOrDefault(x => x.RoomId == id);

            if (roomToReserve == null)
            {
                return HttpNotFound();
            }

            roomToReserve.TempArrivalDate = ArrivalDate;
            roomToReserve.TempDepartureDate = DepartureDate;
            roomToReserve.TempIsPaid = IsPaid;
            roomToReserve.TempTotalPrice = TotalPrice;
            roomToReserve.TempUserId = UserId;
            roomToReserve.Price = Price;

            return View(roomToReserve);
        }

        // POST: RoomAvailabilities/ReserveRoom/5
        [HttpPost, ActionName("ReserveRoom")]
        [ValidateAntiForgeryToken]
        public ActionResult ReserveRoomConfirmed(int? id,DateTime ArrivalDate, DateTime DepartureDate, bool IsPaid, double TotalPrice, string UserId,double Price)
        {
            Room roomToReserve = db.Rooms.SingleOrDefault(x => x.RoomId == id);

            RoomAvailability roomToAddToAvailabilityDB = new RoomAvailability();

            roomToAddToAvailabilityDB.AccomodationId = roomToReserve.AccomodationId;
            roomToAddToAvailabilityDB.ArrivalDate = ArrivalDate;
            roomToAddToAvailabilityDB.DepartureDate = DepartureDate;
            roomToAddToAvailabilityDB.IsPaid = IsPaid;
            roomToAddToAvailabilityDB.RoomId = roomToReserve.RoomId;
            roomToAddToAvailabilityDB.TotalPrice = TotalPrice;
            roomToAddToAvailabilityDB.UserId = UserId;

            

            if (String.IsNullOrEmpty(UserId))
            {

                roomToReserve.TempArrivalDate = ArrivalDate;
                roomToReserve.TempDepartureDate = DepartureDate;
                roomToReserve.TempIsPaid = IsPaid;
                roomToReserve.TempTotalPrice = TotalPrice;
                roomToReserve.TempUserId = UserId;
                roomToReserve.Price = Price;
                ModelState.AddModelError("TempUserId", "You need to be registered user. Please register to reserve room and continue reservation process.");
                return View("ReserveRoom", roomToReserve);
            }
            else
            {
                ApplicationUser userIdForCheck;
                try
                {
                    userIdForCheck = db.Users.First(x => x.Id == UserId);
                }
                catch
                {
                    ModelState.AddModelError("TempUserId", "You need to be registered user. Please register to reserve room and continue reservation process.");
                    return View("ReserveRoom", roomToReserve);
                }
            }


            db.RoomAvailabilities.Add(roomToAddToAvailabilityDB);
            string temp = Convert.ToString(roomToAddToAvailabilityDB.AccomodationId);
            var data = new Message { AccomodationId = roomToAddToAvailabilityDB.AccomodationId, Content = "You have new reservation. Please check your panel", senderName = "New reservation", ReceiverId = temp, SentDate=DateTime.UtcNow, Seen=false, ReceiverName="System Message"};
            db.Messages.Add(data);
            db.SaveChanges();


            ViewBag.reservation = "You have successfully reserved room!";

            return RedirectToAction("Index", "Accomodations", new {reserved=1 });
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
