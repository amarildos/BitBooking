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
    public class PhotosApiController : ApiController
    {
        public PhotosApiController()
        {

            db.Configuration.ProxyCreationEnabled = false;
        }
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PhotosApi
        public IQueryable<Photo> GetImages()
        {
            return db.Images.Include(p => p.Accomodation).Include(p => p.RoomType);
        }

        // GET: api/PhotosApi/5
        [ResponseType(typeof(Photo))]
        public IHttpActionResult GetPhoto(int id,int? AccomodationId, int? RoomTypeId,int? Priority)
        {
            Photo photo = db.Images.Single(
                x => x.AccomodationId == AccomodationId.Value &&
                    x.RoomTypeId == RoomTypeId.Value &&
                    x.Priority == Priority.Value);
            if (photo == null)
            {
                return NotFound();
            }

            return Ok(photo);
        }

        // PUT: api/PhotosApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPhoto(int id, Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != photo.PhotoId)
            {
                return BadRequest();
            }

            db.Entry(photo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(id))
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

        // POST: api/PhotosApi
        [ResponseType(typeof(Photo))]
        public IHttpActionResult PostPhoto(Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Images.Add(photo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = photo.PhotoId }, photo);
        }

        // DELETE: api/PhotosApi/5
        [ResponseType(typeof(Photo))]
        public IHttpActionResult DeletePhoto(int id)
        {
            Photo photo = db.Images.Find(id);
            if (photo == null)
            {
                return NotFound();
            }

            db.Images.Remove(photo);
            db.SaveChanges();

            return Ok(photo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhotoExists(int id)
        {
            return db.Images.Count(e => e.PhotoId == id) > 0;
        }
    }
}