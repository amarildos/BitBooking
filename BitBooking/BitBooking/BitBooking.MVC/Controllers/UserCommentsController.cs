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
    public class UserCommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserComments
        /// <summary>
        /// Display list of User Comments
        /// </summary>
        /// <returns>View list of User Comments</returns>
        public ActionResult Index()
        
        {
           
            return View(db.UserComments.OrderByDescending(x => x.ReportCount).ToList());
        }

        // GET: UserComments/Details/5
        /// <summary>
        /// Display details of User Comments
        /// </summary>
        /// <returns>View User Comments</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserComment userComment = db.UserComments.Find(id);
            if (userComment == null)
            {
                return HttpNotFound();
            }
            return View(userComment);
        }

        // GET: UserComments/Create
        /// <summary>
        /// Create User Comments
        /// </summary>
        /// <returns>View User Comments</returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserCommentId,Comment,ApplicationUserId,UserName,Rating,ReportCount,AccomodationId")] UserComment userComment)
        {
            if (ModelState.IsValid)
            {
                db.UserComments.Add(userComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userComment);
        }

        // GET: UserComments/Edit/5
        /// <summary>
        /// Edit User Comments
        /// </summary>
        /// <returns>View User Comments</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserComment userComment = db.UserComments.Find(id);
            if (userComment == null)
            {
                return HttpNotFound();
            }
            return View(userComment);
        }

        // POST: UserComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserCommentId,Comment,ApplicationUserId,UserName,Rating,ReportCount,AccomodationId")] UserComment userComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userComment);
        }

        // GET: UserComments/Delete/5
        /// <summary>
        /// Delete User Comments
        /// </summary>
        /// <returns>View User Comments</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserComment userComment = db.UserComments.Find(id);
            if (userComment == null)
            {
                return HttpNotFound();
            }
            return View(userComment);
        }

        // POST: UserComments/Delete/5
        /// <summary>
        /// Confirm delete of User Comments
        /// </summary>
        /// <returns>View of Index page</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserComment userComment = db.UserComments.Find(id);
            db.UserComments.Remove(userComment);
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
