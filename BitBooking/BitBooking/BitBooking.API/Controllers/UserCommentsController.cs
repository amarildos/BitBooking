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
    public class UserCommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserComments
        [Authorize]
        public ActionResult Index()
        {

            db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;
            var list = db.UserComments.Where(x => x.ApplicationUserId == userId).Select(x => new { x.Accomodation.AccomodationName, x.Comment, x.Rating });

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _PartialIndex()
        {
            return View(db.UserComments.ToList());
        }

        // GET: UserComments/Details/5
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


        public ActionResult GetComments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userComments = db.UserComments.Where(x=>x.AccomodationId==id).ToList();
            if (userComments == null)
            {
                return HttpNotFound();
            }
            return View(userComments);
        }

        // GET: UserComments/Create
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
                string idFromUsersTable = "";
                string userName = "";
                try
                {
                    idFromUsersTable = db.Users.SingleOrDefault(x => x.Id == userComment.ApplicationUserId).Id;
                    userName = db.Users.SingleOrDefault(x => x.Id == userComment.ApplicationUserId).UserName;
                }
                catch
                {

                    idFromUsersTable = "NONE";
                }

                userComment.ApplicationUserId = idFromUsersTable;
                userComment.UserName = userName;

                if (CheckUser(userComment) == false)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden,"You have not visited this accomodation, or it is too soon for comments!");
                    // send error that user is not available to leave the comment because he is not logged in or something similar
                    //return RedirectToAction("Details", "Accomodations", new { @id = userComment.AccomodationId });
                }
                else if (IsAlreadyCommented(userComment) == true)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.MethodNotAllowed, "You have already commented this accomodation!");
                    // send error that user is ALREADY commented this Accomodation
                    //return RedirectToAction("Details", "Accomodations", new { @id = userComment.AccomodationId });
                }
   

                db.UserComments.Add(userComment);
                db.SaveChanges();
                return RedirectToAction("Details", "UserComments", new { @id = userComment.UserCommentId });
                //return RedirectToAction("Details", "Accomodations", new { @id = userComment.AccomodationId });
            }

            return new HttpStatusCodeResult(HttpStatusCode.MethodNotAllowed, "Placed comment is not valid");
        }

        private bool IsAlreadyCommented(UserComment userComment)
        {
            try
            {
                double test = db.UserComments.SingleOrDefault(x => x.AccomodationId == userComment.AccomodationId && x.ApplicationUserId == userComment.ApplicationUserId).Rating;
            }
            catch
            {
                return false;
            }
            return true;
        }

        [HttpPost]
        public ActionResult Report(int? commentId, string ApplicationUserId, int? AccomodationIdValue)
        {
            if (commentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string testUserName = "null";
            try
            {
                testUserName = db.UserComments.SingleOrDefault(x => x.ApplicationUserId == ApplicationUserId && x.UserCommentId == commentId && x.AccomodationId == AccomodationIdValue.Value).UserName;
            }
            catch 
            { 
            }
            if(testUserName != "null")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "UNABLE TO REPORT");
            }

            int testId = -1;
            try
            {
                testId = db.CommentUserReports.SingleOrDefault(x => x.AccomodationId == AccomodationIdValue && x.ApplicationUserId == ApplicationUserId && x.CommentId == commentId && x.IsReported == true).CommentId;
            }
            catch
            {
                CommentUserReport newCommentUserReport = new CommentUserReport()
                {
                    AccomodationId = AccomodationIdValue.Value,
                    ApplicationUserId = ApplicationUserId,
                    CommentId = commentId.Value,
                    CommentUserReportId = 0,
                    IsReported = true
                };

                db.CommentUserReports.Add(newCommentUserReport);
                db.SaveChanges();

                var userComment = db.UserComments.Find(commentId);
                userComment.ReportCount++;

                db.Entry(userComment).State = EntityState.Modified;
                db.SaveChanges();

                return new HttpStatusCodeResult(HttpStatusCode.OK, "THANK YOU FOR REPORT");
            }

            if (testId != -1 && String.IsNullOrEmpty(ApplicationUserId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "UNABLE TO REPORT");
            }
            else if (testId != -1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "UNABLE TO REPORT");
            }

            return RedirectToAction("Index");
        }

        private bool CheckUser(UserComment userComment)
        {
            int userCounts = 0;
            try
            {
                userCounts = db.RoomAvailabilities.Count(x => x.UserId == userComment.ApplicationUserId && x.ArrivalDate < DateTime.Now && x.AccomodationId == userComment.AccomodationId);
            }
            catch
            {
                return false;
            }
            if (userCounts == 1)
            {
                return true;
            }
            return false;
        }

        // GET: UserComments/Edit/5
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
