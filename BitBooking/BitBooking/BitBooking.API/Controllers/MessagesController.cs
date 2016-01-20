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
    public class MessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Messages
        public ActionResult Index()
        {
            var thisuser = User.Identity.GetUserId();

            db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;
            
            if (accID != 0) {
                string temp = Convert.ToString(accID);
                var tosend = db.Messages.Include(m => m.Accomodation).Where(x => x.AccomodationId == accID && x.ReceiverId == temp).OrderByDescending(x=>x.SentDate);

                return Json(tosend, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var tosend = db.Messages.Include(m => m.Accomodation).Where(x => x.ReceiverId == thisuser).OrderByDescending(x => x.SentDate);

                return Json(tosend, JsonRequestBehavior.AllowGet);
            }

          
        }


        public bool hasnew()
        {
            var thisuser = User.Identity.GetUserId();

            db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = 0;
            try
            {
                accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;
            }
            catch(Exception e)
            {

            }
            
            if (accID != 0)
            {

                string temp = Convert.ToString(accID);
                var tosend = db.Messages.Include(m => m.Accomodation).Where(x => x.AccomodationId == accID && x.ReceiverId == temp&&x.Seen==false).OrderByDescending(x => x.SentDate);

                if (tosend.Count() > 0)
                    return true;
                else
                { return false; }

            }
            else {
                var tosend = db.Messages.Include(m => m.Accomodation).Where(x => x.ReceiverId == thisuser && x.Seen == false).OrderByDescending(x => x.SentDate);

                if (tosend.Count() > 0)
                    return true;
                else
                { return false; }
            
            
            
            }


        }

        [HttpPost]
        public ActionResult Reply(Message message)
        {
            message.SentDate = DateTime.Now;
            var thisuser = User.Identity.GetUserId();

            db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;
            if (accID != 0)
            {
                message.SenderId = Convert.ToString(accID);
                message.senderName = db.Accomodations.Where(x => x.AccomodationId == accID).Select(x => x.AccomodationName).FirstOrDefault();
                message.ReceiverName = db.Users.Where(x => x.Id == message.ReceiverId).Select(x => x.UserName).FirstOrDefault();

            }
            else
            {
                int temp = Convert.ToInt32(message.ReceiverId);
                message.SenderId = thisuser;
                message.senderName = db.Users.Where(x => x.Id == thisuser).Select(x => x.UserName).FirstOrDefault();
                message.ReceiverName = db.Accomodations.Where(x => x.AccomodationId == temp).Select(x => x.AccomodationName).FirstOrDefault();
            
            
            
            };
            db.Messages.Add(message);
            db.SaveChanges();
           


            if (message == null)
            {
                return HttpNotFound();
            }
            
            return Json("Ok");
        }




        [HttpPost]
        public ActionResult Seen(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            message.Seen = true;
           

            if (message == null)
            {
                return HttpNotFound();
            }
            db.Entry(message).State = EntityState.Modified;
            db.SaveChanges();
            return Json("Ok");
        }


        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create()
        {
            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
       public ActionResult Create([Bind(Include = "MessageId,Content,SenderId,ReceiverId,AccomodationId,SentDate,Seen")] Message message)
        {
            message.SentDate = DateTime.Now;
            message.SenderId = User.Identity.GetUserId();
            message.senderName = User.Identity.GetUserName();

            message.ReceiverName = db.Accomodations.Where(x => x.AccomodationId == message.AccomodationId).Select(x => x.AccomodationName).FirstOrDefault();
            
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return Json("Index");
            }

            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName", message.AccomodationId);
            return View(message);
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName", message.AccomodationId);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MessageId,Content,SenderId,ReceiverId,AccomodationId,SentDate,Seen")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName", message.AccomodationId);
            return View(message);
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
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
