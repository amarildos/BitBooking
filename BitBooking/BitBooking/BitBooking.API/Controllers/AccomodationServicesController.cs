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
    public class AccomodationServicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccomodationServices
        public ActionResult Index()
        {

            db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;


            var accomodationServices = db.AccomodationServices.Include(a => a.AccomodationServiceType).Where(a=>a.AccomodationId==accID);
            return Json(accomodationServices.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: AccomodationServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationService accomodationService = db.AccomodationServices.Find(id);
            if (accomodationService == null)
            {
                return HttpNotFound();
            }
            return View(accomodationService);
        }

        // GET: AccomodationServices/Create
        public ActionResult Create()
        {
            ViewBag.AccomodationServiceTypeId = new SelectList(db.AccomodationServiceTypes.OrderBy(x => x.Name), "AccomodationServiceTypeId", "Name");
            return View();
        }

        // POST: AccomodationServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccomodationServiceId,Name,AccomodationId,AccomodationServiceTypeId")] AccomodationService accomodationService)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;

            accomodationService.AccomodationId = (int)accID;

            if (ModelState.IsValid)
            {
                db.AccomodationServices.Add(accomodationService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccomodationServiceTypeId = new SelectList(db.AccomodationServiceTypes.OrderBy(x=>x.Name), "AccomodationServiceTypeId", "Name", accomodationService.AccomodationServiceTypeId).OrderByDescending(x=>x.Text);
            return View(accomodationService);
        }

        // GET: AccomodationServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationService accomodationService = db.AccomodationServices.Find(id);
            if (accomodationService == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccomodationServiceTypeId = new SelectList(db.AccomodationServiceTypes, "AccomodationServiceTypeId", "Name", accomodationService.AccomodationServiceTypeId);
            return View(accomodationService);
        }

        // POST: AccomodationServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccomodationServiceId,Name,AccomodationId,AccomodationServiceTypeId")] AccomodationService accomodationService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accomodationService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccomodationServiceTypeId = new SelectList(db.AccomodationServiceTypes, "AccomodationServiceTypeId", "Name", accomodationService.AccomodationServiceTypeId);
            return View(accomodationService);
        }

        // GET: AccomodationServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccomodationService accomodationService = db.AccomodationServices.Find(id);
            if (accomodationService == null)
            {
                return HttpNotFound();
            }
            return View(accomodationService);
        }

        // POST: AccomodationServices/Delete/5
        [HttpPost, ActionName("Delete")]
      //  [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccomodationService accomodationService = db.AccomodationServices.Find(id);
            db.AccomodationServices.Remove(accomodationService);
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
