using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitBooking.DAL.Models;

namespace BitBooking.MVC.Controllers
{
    [Authorize(Roles = "Administrator, Hotel Manager")]
    public class PromotionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Promotions
        public ActionResult Index()
        {

            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName");
            List<Promotion> firsts = db.Promotions.Where(x=>x.PromotionType==1).ToList();
            List<int> temp=new List<int>();
         
            List<string> firstnames = new List<string>();

            foreach(var item in firsts)
            {
               temp.Add(item.FirstAccomodationId);
                temp.Add(item.SecondAccomodationId);
                temp.Add(item.ThirdAccomodationId);
                foreach(var item2 in temp)
                {
                    var temp4 = db.Accomodations.FirstOrDefault(x => x.AccomodationId == item2);
                    firstnames.Add(temp4.AccomodationName);
                }

            }





            List<Promotion> seconds = db.Promotions.Where(x => x.PromotionType == 2).ToList();
            temp = new List<int>();

            List<string> secondnames = new List<string>();

            foreach (var item in seconds)
            {
                temp.Add(item.FirstAccomodationId);
                temp.Add(item.SecondAccomodationId);
                temp.Add(item.ThirdAccomodationId);
                foreach (var item2 in temp)
                {
                    var temp4 = db.Accomodations.FirstOrDefault(x => x.AccomodationId == item2);
                    secondnames.Add(temp4.AccomodationName);
                }

            }



            List<Promotion> thirds = db.Promotions.Where(x => x.PromotionType == 3).ToList();
            temp = new List<int>();

            List<string> thirdnames = new List<string>();

            foreach (var item in thirds)
            {
                temp.Add(item.FirstAccomodationId);
                temp.Add(item.SecondAccomodationId);
                temp.Add(item.ThirdAccomodationId);
                foreach (var item2 in temp)
                {
                    var temp4 = db.Accomodations.FirstOrDefault(x => x.AccomodationId == item2);
                    thirdnames.Add(temp4.AccomodationName);
                }

            }

            ViewBag.firstlist = firstnames;
            ViewBag.secondlist = secondnames;
            ViewBag.thirdlist = thirdnames;

           
           


            return View();
        }

        // GET: Promotions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            return View(promotion);
        }

        // GET: Promotions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Promotions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PromotionId,FirstAccomodationId,SecondAccomodationId,ThirdAccomodationId,PromotionType")] Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Promotion temp = db.Promotions.Where(x => x.PromotionType == promotion.PromotionType).FirstOrDefault();
                    db.Promotions.Remove(temp);
                }
                catch { }
         
               
                db.Promotions.Add(promotion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(promotion);
        }

        // GET: Promotions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            return View(promotion);
        }

        // POST: Promotions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PromotionId,FirstAccomodationId,SecondAccomodationId,ThirdAccomodationId,PromotionType")] Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promotion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(promotion);
        }

        // GET: Promotions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            return View(promotion);
        }

        // POST: Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Promotion promotion = db.Promotions.Find(id);
            db.Promotions.Remove(promotion);
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
