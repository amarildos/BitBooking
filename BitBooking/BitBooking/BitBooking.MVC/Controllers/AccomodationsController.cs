using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitBooking.DAL.Models;
using BitBooking.MVC.Models;
using Microsoft.AspNet.Identity;

namespace BitBooking.MVC.Controllers
{

    [Authorize(Roles = "Administrator, Hotel Manager")]
    public class AccomodationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
         [Authorize(Roles = "Administrator, Hotel Manager")]
        // GET: Accomodations
        /// <summary>
        /// Display list of Accomodations
        /// </summary>
        /// <returns>View list of Accomodations</returns>
        public ActionResult Index()
        {
            var accomodations = db.Accomodations.Include(a => a.AccomodationInfo).Include(a => a.AccomodationType);
            return View(accomodations.ToList());
        }

        // GET: Accomodations/Details/5
         /// <summary>
         /// Display details of Accomodations
         /// </summary>
         /// <returns>View details of Accomodations</returns>
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
        /// <summary>
        /// Create Accomodations
        /// </summary>
        /// <returns>View Accomodations</returns>
         [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.AccomodationInfoId = new SelectList(db.AccomodationInfoes, "AccomodationInfoId", "Address");
            ViewBag.AccomodationTypeId = new SelectList(db.AccomodationTypes, "AccomodationTypeId", "AccomodationTypeName");
            return View();
        }


        public ActionResult NewAccomodation()
        {
            ViewBag.AccomodationInfoId = new SelectList(db.AccomodationInfoes, "AccomodationInfoId", "Address");
            ViewBag.AccomodationTypeId = new SelectList(db.AccomodationTypes, "AccomodationTypeId", "AccomodationTypeName");
            return View();
        }

        public ActionResult Admin()
        {
            string userid = User.Identity.GetUserId();
            int accID = db.Users.FirstOrDefault(x => x.Id == userid).AccomodationId;
          var hotel=db.Accomodations.Find(accID);
            ViewBag.AccomodationInfoId = new SelectList(db.AccomodationInfoes, "AccomodationInfoId", "Address");
            ViewBag.AccomodationTypeId = new SelectList(db.AccomodationTypes, "AccomodationTypeId", "AccomodationTypeName");
            return View(hotel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewAccomodation(CreatAccomodationInformation createinformation)
        {
            ViewBag.AccomodationInfoId = new SelectList(db.AccomodationInfoes, "AccomodationInfoId", "Address");
            ViewBag.AccomodationTypeId = new SelectList(db.AccomodationTypes, "AccomodationTypeId", "AccomodationTypeName");
            


            if (ModelState.IsValid)
            {
                AccomodationInfo ai = new AccomodationInfo
                {
                    City = createinformation.City,
                    Email = createinformation.Email,
                    Address = createinformation.Address,
                   
                    Country = createinformation.Country,
                    Phone = createinformation.Phone,
                    PostalCode = createinformation.PostalCode

                };
                db.AccomodationInfoes.Add(ai);
                db.SaveChanges();

                var info = db.AccomodationInfoes.Where(x => x.Email == createinformation.Email && x.Phone == createinformation.Phone).FirstOrDefault();
                Accomodation ac = new Accomodation
                {

                    AccomodationName = createinformation.AccomodationName,
                    AccomodationTypeId = createinformation.AccomodationTypeId,
                    Description = createinformation.Description,
                    NumberOfRooms = createinformation.NumberOfRooms,
                    StarRating = createinformation.StarRating,
                    AccomodationInfoId=info.AccomodationInfoId
                    
                };
                db.Accomodations.Add(ac);
                db.SaveChanges();

                var accomo = db.Accomodations.Where(x=>x.AccomodationName==createinformation.AccomodationName && x.Description==createinformation.Description).FirstOrDefault();
                info.AccomodationId = accomo.AccomodationId;
                db.Entry(info).State = EntityState.Modified;
                db.SaveChanges();



                return RedirectToAction("Create", "Tokens", new { email=info.Email});
               }




            //    db.Accomodations.Add(createinformation);
              //  db.SaveChanges();
          //      return RedirectToAction("Index");

            ViewBag.AccomodationTypeId = new SelectList(db.AccomodationTypes, "AccomodationTypeId", "AccomodationTypeName", createinformation.AccomodationTypeId);
            return View(createinformation);
            }

    
   


        // POST: Accomodations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "AccomodationId,AccomodationName,StarRating,NumberOfRooms,AccomodationTypeId,AccomodationInfoId")] Accomodation accomodation)
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
        /// <summary>
        /// Edit Accomodations
        /// </summary>
        /// <returns>View Accomodations</returns>
         [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "AccomodationId,AccomodationName,StarRating,NumberOfRooms,AccomodationTypeId,AccomodationInfoId")] Accomodation accomodation)
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
        /// <summary>
        /// Delete Accomodations
        /// </summary>
        /// <returns>View Accomodations</returns>
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
        /// <summary>
        /// Confirm delete of Accomodations
        /// </summary>
        /// <returns>View of Index page</returns>
         [Authorize(Roles = "Administrator")]
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
