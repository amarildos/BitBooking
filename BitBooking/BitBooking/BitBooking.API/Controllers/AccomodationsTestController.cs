using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BitBooking.DAL.Models;

namespace BitBooking.API.Controllers
{
    public class AccomodationsTestController : ApiController
    {
        public AccomodationsTestController()
        {

            db.Configuration.ProxyCreationEnabled = false;
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AccomodationsTest
        public IQueryable<Object> GetAccomodations()
        {
            return db.Accomodations.Include(a => a.AccomodationInfo).Include(a => a.AccomodationType)
                .Select(x=> new{x.AccomodationId, x.AccomodationInfo,x.AccomodationType,x.AccomodationName,x.ListOfPhotos,x.Description,x.StarRating,x.NumberOfRooms});
        }

        // GET: api/AccomodationsTest/5
        [ResponseType(typeof(Accomodation))]
        public IHttpActionResult GetAccomodation(int id)
        {
            List<Room> rooms_temp= new List<Room>();
            List<AccomodationService> services_temp = new List<AccomodationService>();
            List<AccomodationFacility> facilities_temp = new List<AccomodationFacility>();
           
            Accomodation accomodation = db.Accomodations.Include(x => x.ListOfPhotos)
                                                        .Include(x => x.ListOfRooms)
                                                        .Include(x => x.ListOfAccomodationFacilities)
                                                        .Include(x=>x.ListOfAccomodationServices)
                                                        .Include(x => x.AccomodationInfo)
                                                        .Include(x=> x.AccomodationType)
                                                        .FirstOrDefault(x => x.AccomodationId == id);

            //foreach(var facility in accomodation.ListOfAccomodationFacilities)
            //{
            //    AccomodationFacility newFacility = db.AccomodationFacilities.FirstOrDefault(x=> x.AccomodationId == facility.AccomodationId && x.AccomodationFacilityId == facility.AccomodationFacilityId);
            //    try
            //    {
            //        newFacility.PhotoUrl = db.Images.FirstOrDefault(x => x.AccomodationId == facility.AccomodationId && x.Priority == 99 && x.FacilityId == facility.AccomodationFacilityId).PhotoUrl;
            //    }
            //    catch(Exception e)
            //    {
            //        newFacility.PhotoUrl = "/Images/defaultAccomodationPhoto.jpg";
            //    }

            //    facilities_temp.Add(newFacility);
            //}

            //accomodation.ListOfAccomodationFacilities = facilities_temp;

            foreach(var hs in accomodation.ListOfRooms)
            {
                rooms_temp.Add(db.Rooms.Include(x => x.RoomType).FirstOrDefault(x => hs.RoomId == x.RoomId));
            }
            accomodation.ListOfRooms = rooms_temp;


            foreach (var hs in accomodation.ListOfAccomodationServices)
            {
                services_temp.Add(db.AccomodationServices.Include(x => x.AccomodationServiceType).FirstOrDefault(x => x.AccomodationServiceId == hs.AccomodationServiceId));
            }

            
            accomodation.ListOfAccomodationServices = services_temp;
            
            if (accomodation == null)
            {
                return NotFound();
            }

            return Ok(accomodation);
        }


        // PUT: api/AccomodationsTest/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccomodation(int id, Accomodation accomodation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accomodation.AccomodationId)
            {
                return BadRequest();
            }

            db.Entry(accomodation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccomodationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AccomodationsTest
        [ResponseType(typeof(Accomodation))]
        public IHttpActionResult PostAccomodation(Accomodation accomodation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Accomodations.Add(accomodation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = accomodation.AccomodationId }, accomodation);
        }

        // DELETE: api/AccomodationsTest/5
        [ResponseType(typeof(Accomodation))]
        public IHttpActionResult DeleteAccomodation(int id)
        {
            Accomodation accomodation = db.Accomodations.Find(id);
            if (accomodation == null)
            {
                return NotFound();
            }

            db.Accomodations.Remove(accomodation);
            db.SaveChanges();

            return Ok(accomodation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccomodationExists(int id)
        {
            return db.Accomodations.Count(e => e.AccomodationId == id) > 0;
        }
    }
}