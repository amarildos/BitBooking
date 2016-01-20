using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitBooking.DAL.Models;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;

namespace BitBooking.API.Controllers
{
    public class AccomodationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Accomodations
        public ActionResult Index(string searchString, DateTime? arrivalDate, DateTime? departureDate, string TempUserId, int? reserved)
        {
            
            string userIdentity = User.Identity.Name;   //test user identitye

            if (reserved == 1)
            { ViewBag.reservation = "You have successfully reserved room!"; }
            var accomodations = db.Accomodations.Include(a => a.AccomodationInfo).Include(a => a.AccomodationType);

            //checking if the search string is NULL or EMPTY and DATES ARE EMPTY SO WE COULD RETURN ONLY ACCOMODATIONS WHICH SATIFSY SEARCH CRITERIA
            if (SearchStringCheck(searchString) == true && (arrivalDate == null || departureDate == null))
            {
                var accomdationQueryForSearch = accomodations.Where(x => x.AccomodationName.ToLower().Contains(searchString.ToLower()) || x.AccomodationInfo.City.ToLower().Contains(searchString.ToLower())).ToList();
                if (accomdationQueryForSearch.Count > 0)
                {
                    ModelState.AddModelError("arrivalDate", "Arrival date needs to be entered in order to find suitable accomodation.");
                    ModelState.AddModelError("departureDate", "Departure date needs to be entered in order to find suitable accomodation. Withour arrival and departure dates entered, you can see the list of found accomodations by accomodation name or city.");
                    return View(accomdationQueryForSearch.ToList());
                }
                else
                {
                    ModelState.AddModelError("searchString", "There are no accomodations which suites your search criteria.");
                    ModelState.AddModelError("arrivalDate", "Arrival date needs to be entered in order to find suitable accomodation.");
                    ModelState.AddModelError("departureDate", "Departure date needs to be entered in order to find suitable accomodation.");
                    return View(accomdationQueryForSearch.ToList());
                    //return View("NoHotelsFound");
                }
            }
                        
            // searching the Accomodations DataBase to find out are there any found accomodations which satisfy search string
            var accomdationQuery = accomodations.Where(x => x.AccomodationName.ToLower().Contains(searchString.ToLower()) || x.AccomodationInfo.City.ToLower().Contains(searchString.ToLower())).ToList();

            //checking the search dates
            if(arrivalDate != null && departureDate != null)
            {
                if (CheckIfArrivalDateIsBeforeToday(arrivalDate.Value, departureDate.Value) == false)
                {
                    ModelState.AddModelError("arrivalDate", "Arrival date can not be before today. Please search again!");
                    if (CheckIfArrivalDateIsAfterDepartueDate(arrivalDate.Value, departureDate.Value) == false)
                    {
                        ModelState.AddModelError("arrivalDate", "Arrival date can not be after or equal departure date. Please search again!");
                    }
                    return View(accomodations.ToList());
                }
                else if (CheckIfArrivalDateIsAfterDepartueDate(arrivalDate.Value, departureDate.Value) == false)
                {
                    ModelState.AddModelError("arrivalDate", "Arrival date can not be after or equal departure date. Please search again!");
                    return View(accomodations.ToList());
                }
                {
                    if (accomdationQuery.Count > 0)
                    {
                        return FindAvailableRooms(accomdationQuery, arrivalDate.Value, departureDate.Value, TempUserId);
                    }
                    else
                    {
                        ModelState.AddModelError("searchString", "There are no accomodations which suites your search criteria.");
                        return View(accomdationQuery.ToList());
                        //return View("NoRoomsFound");
                    }
                }
            }

            return View(accomodations.ToList());
        }

        //checking if the search string is NULL or EMPTY
        //if it is NULL or EMPTY, it will return the NON-SEARCHED list of accomodations
        public bool SearchStringCheck(string searchString)
        {
            if (String.IsNullOrEmpty(searchString) == true)
            {
                return false;
            }
            return true;
        }

        public bool CheckIfArrivalDateIsAfterDepartueDate(DateTime arrivalDate, DateTime departureDate)
        {
            if (arrivalDate >= departureDate)
            {
                return false;
            }
            return true;
        }

        public bool CheckIfArrivalDateIsBeforeToday(DateTime arrivalDate, DateTime departureDate)
        {
            if (arrivalDate < DateTime.Today)
            {
                return false;
            }
            return true;
        }

               public ActionResult SponsoredList(int? id)
        {

            db.Configuration.ProxyCreationEnabled = false;
        List<Accomodation> sponsored=new List<Accomodation>();
            Promotion temp=db.Promotions.Where(x=>x.PromotionType==id).FirstOrDefault();
            try{
                sponsored.Add(db.Accomodations.Where(x => x.AccomodationId == temp.FirstAccomodationId).Include(x=>x.AccomodationInfo).Include(x=>x.ListOfPhotos).FirstOrDefault());



                sponsored.Add(db.Accomodations.Where(x => x.AccomodationId == temp.SecondAccomodationId).Include(x => x.AccomodationInfo).Include(x => x.ListOfPhotos).FirstOrDefault());
                sponsored.Add(db.Accomodations.Where(x => x.AccomodationId == temp.ThirdAccomodationId).Include(x => x.AccomodationInfo).Include(x => x.ListOfPhotos).FirstOrDefault());

            }
            catch{}
            var list = JsonConvert.SerializeObject(sponsored,
        Formatting.None,
        new JsonSerializerSettings()
        {
            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        });
            return Content(list, "application/json");
       // return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ActionResult FindAvailableRooms(List<Accomodation> accomdationQuery,DateTime arrivalDate, DateTime departureDate,string TempUserId)
        {
            //referencing the roomAvailabilityController so we could access the Action inside the AccomodationController
            var roomAvailabilityController = new RoomAvailabilitiesController();    //RoomAvailibility CONTROLLER Object

            List<Room> roomList = new List<Room>();

            //iterating trough the list of accomodations and their rooms
            for (int i = 0; i < accomdationQuery.Count; i++)
            {
                foreach (var room in accomdationQuery[i].ListOfRooms)
                {
                    Room roomToAdd = roomAvailabilityController.GetAvailabilityAndPrice(room.AccomodationId, room.RoomId, arrivalDate, departureDate, TempUserId);
                    if (roomToAdd != null)
                    {
                        roomToAdd.TempReservationString = System.Guid.NewGuid().ToString();
                        roomList.Add(roomToAdd);
                    }
                }
            }

            if (roomList.Count == 0)
            {
                ModelState.AddModelError("searchString", "There are no available rooms found which suites your search criteria. Your arrival date was: " + arrivalDate.ToShortDateString() + " , and departure date: " + departureDate.ToShortDateString() + " . Please search again.");
                return View("Index", accomdationQuery);
                //return View("NoRoomsFound");
            }
            

            List<int> roomIds = new List<int>();

            foreach (var room in roomList)
            {
                roomIds.Add(room.RoomId);
            }

            //IQueryable<Room> query = db.Rooms.Include(r => r.Accomodation).Include(r => r.RoomType);
            IQueryable<Room> query = db.Rooms.Include(r => r.RoomType);
            List<Room> queriedRooms = new List<Room>();

            foreach (int id in roomIds)
            {
                queriedRooms.AddRange(query.Where(x => x.RoomId == id).ToList());
            }

            for (int i = 0; i < queriedRooms.Count; i++)
            {
                queriedRooms[i].TempTotalPrice = roomList[i].TempTotalPrice;
                queriedRooms[i].TempReservationString = roomList[i].TempReservationString;
                queriedRooms[i].TempArrivalDate = roomList[i].TempArrivalDate;
                queriedRooms[i].TempDepartureDate = roomList[i].TempDepartureDate;
                queriedRooms[i].Price = roomList[i].Price;
                queriedRooms[i].TempUserId = roomList[i].TempUserId;
            }

            if (queriedRooms != null)
            {
                return ShowAvailableRooms(queriedRooms);
            }
            return View("NoRoomsFound");
        }

        public ActionResult NoRoomsFound()
        {
            return View();
        }

        public ActionResult NoHotelsFound()
        {
            return View();
        }

        public ActionResult ShowAvailableRooms(List<Room> listOfRooms)
        {
            return View("ShowAvailableRooms", listOfRooms);
        }

        // GET: Accomodations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accomodation accomodation = db.Accomodations.Find(id);
            if (accomodation == null)
            {
                return HttpNotFound();
            }
            return View(accomodation);
        }

        // GET: Accomodations/Create
        public ActionResult Create()
        {
            ViewBag.AccomodationInfoId = new SelectList(db.AccomodationInfoes, "AccomodationInfoId", "Address");
            ViewBag.AccomodationTypeId = new SelectList(db.AccomodationTypes, "AccomodationTypeId", "AccomodationTypeName");
            return View();
        }


        public ActionResult GetName()
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var userId = this.User.Identity.GetUserId();

                int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;
                var data = db.Accomodations.Where(x => x.AccomodationId == accID).FirstOrDefault();
                return Json(data, JsonRequestBehavior.AllowGet);
            }

            catch(Exception e)
            {
                return View("Error");
            
            };
        }


        // POST: Accomodations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccomodationId,AccomodationName,StarRating,NumberOfRooms,Description,AccomodationTypeId,AccomodationInfoId")] Accomodation accomodation)
        {
            if (ModelState.IsValid)
            {
                db.Accomodations.Add(accomodation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccomodationInfoId = new SelectList(db.AccomodationInfoes, "AccomodationInfoId", "Address", accomodation.AccomodationInfoId);
            ViewBag.AccomodationTypeId = new SelectList(db.AccomodationTypes, "AccomodationTypeId", "AccomodationTypeName", accomodation.AccomodationTypeId);
            return View(accomodation);
        }

        // GET: Accomodations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accomodation accomodation = db.Accomodations.Find(id);
            if (accomodation == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccomodationInfoId = new SelectList(db.AccomodationInfoes, "AccomodationInfoId", "Address", accomodation.AccomodationInfoId);
            ViewBag.AccomodationTypeId = new SelectList(db.AccomodationTypes, "AccomodationTypeId", "AccomodationTypeName", accomodation.AccomodationTypeId);
            return View(accomodation);
        }

        // POST: Accomodations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccomodationId,AccomodationName,StarRating,NumberOfRooms,Description,AccomodationTypeId,AccomodationInfoId")] Accomodation accomodation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accomodation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccomodationInfoId = new SelectList(db.AccomodationInfoes, "AccomodationInfoId", "Address", accomodation.AccomodationInfoId);
            ViewBag.AccomodationTypeId = new SelectList(db.AccomodationTypes, "AccomodationTypeId", "AccomodationTypeName", accomodation.AccomodationTypeId);
            return View(accomodation);
        }

        // GET: Accomodations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accomodation accomodation = db.Accomodations.Find(id);
            if (accomodation == null)
            {
                return HttpNotFound();
            }
            return View(accomodation);
        }

        // POST: Accomodations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accomodation accomodation = db.Accomodations.Find(id);
            db.Accomodations.Remove(accomodation);
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

