using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BitBooking.API.Models;
using BitBooking.DAL.Models;
using System.Data;
using System.Data.Entity;

namespace BitBooking.API.Controllers
{
    public class SearchController : ApiController
    {
        private BitBooking.DAL.Models.ApplicationDbContext db = new BitBooking.DAL.Models.ApplicationDbContext();
        // GET: api/Search
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Search/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Search
        public List<RoomSearchModel> Post(SearchModel search)
        {
            List<RoomSearchModel> roomsToFind = new List<RoomSearchModel>();
            
            string userIdentity = User.Identity.Name;   //GET REGISTERED USER USERNAME (email)
            string searchString = search.content;       //Accomodation name for search
            string searchCity = search.SearchCity;
            string searchCountry = search.SearchCountry;
            DateTime arrivalDate = search.Arrival_date;
            DateTime departureDate = search.Departure_date;
            int peopleCapacity = search.peopleCapacity;

            //var townsInCountry = db.AccomodationInfoes.Where(x => x.Country.ToLower().Contains(searchCountry.ToLower())).ToList();

            //bool errorInCitySearch = false;
            //foreach(var town in townsInCountry)
            //{
            //    if(town.City == searchCity)
            //    {
            //        errorInCitySearch = true;
            //    } 
            //}

            //if(errorInCitySearch == false)
            //{
            //    List<RoomSearchModel> rm = new List<RoomSearchModel>();
            //    rm.Add(new RoomSearchModel 
            //    { 
            //        AccomodationName = "You have selected city which is not in " + searchCountry,
            //        AccomodationId = 789
            //    });
            //    return rm;
            //}

            string TempUserId = FindUserId(userIdentity);   //GET THE USER ID BY USERNAME (email)

            var accomodations = db.Accomodations.Include(a => a.AccomodationInfo).Include(a => a.AccomodationType);

            

            //checking if the search string is NULL or EMPTY and DATES ARE EMPTY SO WE COULD RETURN ONLY ACCOMODATIONS WHICH SATIFSY SEARCH CRITERIA
            if (SearchStringCheck(searchString) == true && (arrivalDate == null || departureDate == null))
            {
                var accomdationQueryForSearch = accomodations.Where(x => x.AccomodationName.ToLower().Contains(searchString.ToLower()) || x.AccomodationInfo.City.ToLower().Contains(searchCity.ToLower()) || x.AccomodationInfo.Country.ToLower().Contains(searchCountry)).ToList();

                if (accomdationQueryForSearch.Count > 0)
                {
                    return roomsToFind; //VRAĆA PRAZNU LISTU ROBA (API) NOVO
                }
                else
                {
                    return roomsToFind; //VRAĆA PRAZNU LISTU ROBA (API) NOVO
                }
            }

            // searching the Accomodations DataBase to find out are there any found accomodations which satisfy search string
            //var accomdationQuery = accomodations.Where(x => x.AccomodationName.ToLower().Contains(searchString.ToLower()) || x.AccomodationInfo.City.ToLower().Contains(searchCity.ToLower()) || x.AccomodationInfo.Country.ToLower().Contains(searchCountry)).ToList();

            //IQueryable<Accomodation> query = accomodations;

            if (searchCountry == "All Countries" && searchCity == "All Cities" && String.IsNullOrEmpty(searchString) == false)
            {
                accomodations = accomodations.Where(x => x.AccomodationName.ToLower() == searchString.ToLower());
            }
            if (searchCountry == "All Countries" && searchCity != "All Cities" && String.IsNullOrEmpty(searchString) == false)
            {
                accomodations = accomodations.Where(x => x.AccomodationName.ToLower() == searchString.ToLower() && x.AccomodationInfo.City.ToLower() == searchCity.ToLower());
            }
            if (searchCountry != "All Countries" && searchCity == "All Cities" && String.IsNullOrEmpty(searchString) == false)
            {
                accomodations = accomodations.Where(x => x.AccomodationInfo.Country.ToLower() == searchCountry.ToLower() && x.AccomodationName.ToLower() == searchString.ToLower());
            }
            if (searchCountry != "All Countries" && searchCity != "All Cities" && String.IsNullOrEmpty(searchString) == false)
            {
                accomodations = accomodations.Where(x => x.AccomodationInfo.Country.ToLower() == searchCountry.ToLower() && x.AccomodationInfo.City.ToLower() == searchCity.ToLower() && x.AccomodationName.ToLower() == searchString.ToLower());
            }
            if (searchCountry != "All Countries" && searchCity != "All Cities" && String.IsNullOrEmpty(searchString) == true)
            {
                accomodations = accomodations.Where(x => x.AccomodationInfo.Country.ToLower() == searchCountry.ToLower() && x.AccomodationInfo.City.ToLower() == searchCity.ToLower());
            }
            if (searchCountry != "All Countries" && searchCity == "All Cities" && String.IsNullOrEmpty(searchString) == true)
            {
                accomodations = accomodations.Where(x => x.AccomodationInfo.Country.ToLower() == searchCountry.ToLower());
            }
            if (searchCountry == "All Countries" && searchCity != "All Cities" && String.IsNullOrEmpty(searchString) == true)
            {
                accomodations = accomodations.Where(x => x.AccomodationInfo.City.ToLower() == searchCity.ToLower());
            }


            if(search.NumberOfStarsSearch != 0)
            {
                accomodations = accomodations.Where(x => x.StarRating == search.NumberOfStarsSearch);
            }

            //checking the search dates
            if (arrivalDate != null && departureDate != null)
            {
                if (CheckIfArrivalDateIsBeforeToday(arrivalDate, departureDate) == false)
                {
                    if (CheckIfArrivalDateIsAfterDepartueDate(arrivalDate, departureDate) == false)
                    {
                        return roomsToFind; //THIS SOLUTION IS NOT OK
                    }
                    //return View(accomodations.ToList());
                    return roomsToFind; //VRAĆA PRAZNU LISTU ROBA (API) NOVO
                }
                else if (CheckIfArrivalDateIsAfterDepartueDate(arrivalDate, departureDate) == false)
                {
                    return roomsToFind; //VRAĆA PRAZNU LISTU ROBA (API) NOVO
                }
                {
                    if (accomodations.ToList().Count > 0)
                    {
                        roomsToFind = FindAvailableRooms(accomodations.ToList(), arrivalDate, departureDate, TempUserId, peopleCapacity,search.TotalPriceSearch);
                        
                        foreach(var room in roomsToFind)
                        {
                            var imageUrl = "";
                            try
                            {
                                imageUrl = db.Images.FirstOrDefault(x => x.RoomTypeId == room.RoomTypeId && x.AccomodationId == room.AccomodationId && x.Priority == 0).PhotoUrl;
                            }
                            catch (Exception e)
                            {
                                imageUrl = "/Images/defaultAccomodationPhotoSmall.jpg";
                            }
                            
                            room.PhotoUrl = imageUrl;
                        }

                        return roomsToFind;
                    }
                    else
                    {
                        return roomsToFind; //VRAĆA PRAZNU LISTU ROBA (API) NOVO
                    }
                }
            }

            return roomsToFind; //OVDJE NIKAD NEĆE DOĆI (API)
           
        }



        private string FindUserId(string UserName)
        {
            string userId = "";
            try
            {
                userId = db.Users.First(x => x.Email == UserName).Id;
            }
            catch
            {
                //ModelState.AddModelError("TempUserId", "You need to be registered user. Please register to reserve room and continue reservation process.");
                return "unregistered";
            }
            return userId;
        }

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
            if (arrivalDate.Date < DateTime.Today)
            {
                return false;
            }
            return true;
        }

        public List<RoomSearchModel> FindAvailableRooms(List<Accomodation> accomdationQuery, DateTime arrivalDate, DateTime departureDate, string TempUserId, int peopleCapacity, double totalPrice)
        {
            List<RoomSearchModel> roomList = new List<RoomSearchModel>();

            //iterating trough the list of accomodations and their rooms
            for (int i = 0; i < accomdationQuery.Count; i++)
            {
                foreach (var room in accomdationQuery[i].ListOfRooms)
                {
                    RoomSearchModel roomToAdd = GetAvailabilityAndPrice(room.AccomodationId, room.RoomId, arrivalDate, departureDate, TempUserId);
                    if(totalPrice != 0)
                    {
                        if (roomToAdd != null && roomToAdd.RoomCapacity == peopleCapacity && roomToAdd.TempTotalPrice < totalPrice)
                        {
                            roomToAdd.AccommodationStars = room.Accomodation.StarRating;
                            roomToAdd.TempReservationString = System.Guid.NewGuid().ToString();
                            roomList.Add(roomToAdd);
                        }
                    }
                    if (totalPrice == 0)
                    {
                        if (roomToAdd != null && roomToAdd.RoomCapacity == peopleCapacity)
                        {
                            roomToAdd.AccommodationStars = room.Accomodation.StarRating;
                            roomToAdd.TempReservationString = System.Guid.NewGuid().ToString();
                            roomList.Add(roomToAdd);
                        }
                    }
                }
            }

            if (roomList.Count == 0)
            {
                //ModelState.AddModelError("searchString", "There are no available rooms found which suites your search criteria. Your arrival date was: " + arrivalDate.ToShortDateString() + " , and departure date: " + departureDate.ToShortDateString() + " . Please search again.");

                return roomList;
                //return View("Index", accomdationQuery);   //BILO JE OVO
                
            }


            List<int> roomIdsFromFoundRooms = new List<int>();

            foreach (var room in roomList)
            {
                roomIdsFromFoundRooms.Add(room.RoomId);
            }

            //IQueryable<Room> query = db.Rooms.Include(r => r.Accomodation).Include(r => r.RoomType);
            IQueryable<Room> query = db.Rooms.Include(r => r.RoomType);
            List<Room> queriedRooms = new List<Room>();

            foreach (int id in roomIdsFromFoundRooms)
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
                //queriedRooms[i].Accomodation.StarRating = roomList[i].AccommodationStars; //
            }

            for (int i = 0; i < queriedRooms.Count;i++)
            {
                roomList[i].AccomodationName = queriedRooms[i].Accomodation.AccomodationName;
                roomList[i].RoomType_Name = queriedRooms[i].RoomType.RoomTypeName;
            }

            if (queriedRooms != null)
            {
                return roomList;    //ako lista SOBA nije lista prazna
            }

            return roomList;    //VJEROVATNO VRAĆA PRAZNU LISTU SOBA

            //return View("NoRoomsFound");  //BILO JE OVO
        }


        public RoomSearchModel GetAvailabilityAndPrice(int accomodationId, int roomId, DateTime? from, DateTime? to, string UserId)   //nedostaje UserID da bi se znalo za koga se soba rezerviše
        {
            var currentBooking = db.RoomAvailabilities.Where((x => x.RoomId == roomId && x.AccomodationId == accomodationId &&
                (from <= x.DepartureDate && from >= x.ArrivalDate || x.ArrivalDate <= to && x.ArrivalDate >= from)))
                .ToList();

            var roomForInformation = db.Rooms.Where(x => x.RoomId == roomId).SingleOrDefault();

            var roomTypeId = roomForInformation.RoomTypeId;
            var pricePerNight = roomForInformation.Price;
            var numberOfRooms = roomForInformation.NumberOfRooms;
            var roomCapacity = roomForInformation.RoomCapacity;
            
            //START - NEW TEST

            if (currentBooking.Count >= numberOfRooms)
            {
                return null;
            }

            //END - NEW TEST

            RoomSearchModel foundRoom = new RoomSearchModel()
            {
                AccomodationId = accomodationId,
                Price = pricePerNight,
                RoomId = roomId,
                RoomTypeId = roomTypeId,
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
                    totalPrice += pricePerNight;
                }
                totalPrice += pricePerDay;
                currentDay = currentDay.AddDays(1);
            }

            double totalDays = (to.Value - from.Value).TotalDays;

            foundRoom.Price = totalPrice / totalDays;

            foundRoom.TempTotalPrice = totalPrice;

            return foundRoom;
        }
        
        [Route("api/Search/PostReserveRoom")]
        public IHttpActionResult PostReserveRoom([FromBody]ReserveConfirmationModel model)
        {

            if (model.id == null)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The room is not found. Bad request!"));
            }
            Room roomToReserve = db.Rooms.SingleOrDefault(x => x.RoomId == model.id);

            if (roomToReserve == null)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "The room is already taken. Please make sure to search for another room."));
            }
            RoomSearchModel roomConfirmation = new RoomSearchModel();

            roomConfirmation.AccomodationId = roomToReserve.AccomodationId;
            roomConfirmation.AccomodationName = roomToReserve.Accomodation.AccomodationName;
            roomConfirmation.NumberOfRooms = roomToReserve.NumberOfRooms;
            roomConfirmation.Price = model.Price;   //
            roomConfirmation.RoomCapacity = roomToReserve.RoomCapacity;
            roomConfirmation.RoomDetails = roomToReserve.RoomDetails;
            roomConfirmation.RoomId = model.id.Value;
            roomConfirmation.RoomType_Name = roomToReserve.RoomType.RoomTypeName;
            roomConfirmation.RoomTypeId = roomToReserve.RoomTypeId;
            roomConfirmation.TempArrivalDate = model.ArrivalDate;   //
            roomConfirmation.TempDepartureDate = model.DepartureDate;   //
            roomConfirmation.TempIsPaid = model.IsPaid; //
            roomConfirmation.TempReservationString = null;
            roomConfirmation.TempTotalPrice = model.TotalPrice; //
            roomConfirmation.TempUserId = model.UserId; //
            try
            {
                roomConfirmation.PhotoUrl = db.Images.FirstOrDefault(
                    x => x.AccomodationId == roomConfirmation.AccomodationId &&
                    x.RoomTypeId == roomConfirmation.RoomTypeId &&
                    x.Priority == 0)
                    .PhotoUrl;
            }
            catch (Exception e)
            {
                roomConfirmation.PhotoUrl = "https://www.roominreturn.nl/static/images/room-default.svg";
            }
            
            return Ok(roomConfirmation);
        }



        [Route("api/Search/PostReserveRoomConfirmation")]
        public IHttpActionResult ReserveRoomConfirmed([FromBody]ReserveConfirmationModel model)
        {
            string userEmail = User.Identity.Name;
            Room roomToReserve = db.Rooms.SingleOrDefault(x => x.RoomId == model.id);

            RoomAvailability roomToAddToAvailabilityDB = new RoomAvailability();

            roomToAddToAvailabilityDB.AccomodationId = roomToReserve.AccomodationId;
            roomToAddToAvailabilityDB.ArrivalDate = model.ArrivalDate;
            roomToAddToAvailabilityDB.DepartureDate = model.DepartureDate;
            roomToAddToAvailabilityDB.IsPaid = model.IsPaid;
            roomToAddToAvailabilityDB.RoomId = roomToReserve.RoomId;
            roomToAddToAvailabilityDB.TotalPrice = model.TotalPrice;
            roomToAddToAvailabilityDB.UserId = model.UserId;
            roomToAddToAvailabilityDB.UserEmail = userEmail;



            if (model.UserId == "unregistered")
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please sign in or register in order to continue reservation process."));
            }
            else
            {
                BitBooking.DAL.Models.ApplicationUser userIdForCheck;
                try
                {
                    userIdForCheck = db.Users.First(x => x.Id == model.UserId);
                }
                catch
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please sign in or register in order to continue reservation process."));
                }
            }
            string temp = Convert.ToString(roomToAddToAvailabilityDB.AccomodationId);
            var data = new Message { AccomodationId = roomToAddToAvailabilityDB.AccomodationId, Content = "You have new reservation. Please check your panel", senderName = "New reservation", ReceiverId = temp, SentDate = DateTime.Now, Seen = false, ReceiverName = "System Message" };
            db.Messages.Add(data);

            db.RoomAvailabilities.Add(roomToAddToAvailabilityDB);
            db.SaveChanges();



            return Ok("You have sucessfully reserved a room!");
        }

        // PUT: api/Search/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Search/5
        public void Delete(int id)
        {
        }
    }
}
