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
using BitBooking.API.Models;

namespace BitBooking.API.Controllers
{
    public class UserCommentsAPIController : ApiController
    {
        //public int UserCommentId { get; set; }

        //[Required]
        //[MaxLength(500)]
        //[DataType(DataType.MultilineText)]
        //public string Comment { get; set; }

        //public string ApplicationUserId { get; set; }

        //[Required]
        //public string UserName { get; set; }

        //[Range(1, 5)]
        //public double Rating { get; set; }

        //public int ReportCount { get; set; }

        //public int AccomodationId { get; set; }

        //public virtual Accomodation Accomodation { get; set; }

        private BitBooking.DAL.Models.ApplicationDbContext db = new BitBooking.DAL.Models.ApplicationDbContext();

        //OLD USER COMMENTS ACIONTS - START

        [Route("api/UserCommentsAPI/PostComments")]
        public IHttpActionResult PostComments([FromBody]UserCommentsAPI userComment)
        {
            string userName = User.Identity.Name;
            if (String.IsNullOrEmpty(userName))
            {
                return BadRequest("You can not comment, please register or sing in!");
            }

            string userId = "";

            try
            {
                userId = db.Users.SingleOrDefault(x => x.Email == userName).Id;
            }
            catch
            {

                userId = "NONE";
            }

            userComment.ApplicationUserId = userId;
            userComment.UserName = userName;

            if (CheckUser(userComment) == false)
            {
                return BadRequest("Posting comment is not allowed. Please check arrival date, payment or you have not visited this accommodation!");
            }
            else if (IsAlreadyCommented(userComment) == true)
            {
                return BadRequest("You have already commented this accomodation!");
            }
            
            UserComment userCommentForSave = new UserComment();
            userCommentForSave.AccomodationId = userComment.AccomodationId;
            userCommentForSave.ApplicationUserId = userComment.ApplicationUserId;
            userCommentForSave.Comment = userComment.Comment;
            userCommentForSave.Rating = userComment.Rating;
            userCommentForSave.UserName = userComment.UserName;

            db.UserComments.Add(userCommentForSave);
            db.SaveChanges();
            
            return Ok("Thank you for Your comment. Have a nice day.");
        }

        private bool IsAlreadyCommented(UserCommentsAPI userComment)
        {
            try
            {
                double test = db.UserComments
                    .SingleOrDefault(x => x.AccomodationId == userComment.AccomodationId &&
                    x.ApplicationUserId == userComment.ApplicationUserId)
                    .Rating;
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool CheckUser(UserCommentsAPI userComment)
        {
            int userCounts = 0;
            try
            {
                userCounts = db.RoomAvailabilities
                    .Count(x => x.UserId == userComment.ApplicationUserId &&
                    x.ArrivalDate < DateTime.Now &&
                    x.AccomodationId == userComment.AccomodationId&&
                    x.IsPaid == true);
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

        [Route("api/UserCommentsAPI/PostCommentFromAccomodation")]
        public IHttpActionResult PostCommentFromAccomodation([FromBody]UserCommentsAPI userComment)
        {

            if (userComment.AccomodationId == 0)
            {
                return BadRequest("Accomodation ID is NULL !");
            }
            var userComments = db.UserComments.Where(x => x.AccomodationId == userComment.AccomodationId).ToList();
            List<UserCommentsAPI> userCommentsDto = new List<UserCommentsAPI>();

            foreach(var comment in userComments)
            {
                userCommentsDto.Add(
                new UserCommentsAPI
                {
                    AccomodationId = comment.AccomodationId,
                    ApplicationUserId = comment.ApplicationUserId,
                    Comment = comment.Comment,
                    Rating = comment.Rating,
                    ReportCount = comment.ReportCount,
                    UserCommentId = comment.UserCommentId,
                    UserName = comment.UserName
                });
            }

            if (userCommentsDto == null)
            {
                return BadRequest("There are not comments for this accommodation!");
            }
            return Ok(userCommentsDto);
        }

        [Route("api/UserCommentsAPI/PostReport")]
        public IHttpActionResult PostReport([FromBody]UserCommentReportAPI report)
        {
            string userName = User.Identity.Name;

            string userId = "";

            try
            {
                userId = db.Users.SingleOrDefault(x => x.Email == userName).Id;
            }
            catch
            {
                return BadRequest("Please sign in to report this comment!");
            }

            //userComment.ApplicationUserId = userId;

            if (report.CommentId == 0)
            {
                return BadRequest("This comment does not exist for this accomodation.");
            }

            string testAppUserId = "null";
            try
            {
                testAppUserId = db.UserComments.Single(x => x.ApplicationUserId == report.ApplicationUserId && x.UserCommentId == report.CommentId && x.AccomodationId == report.AccomodationIdValue).ApplicationUserId;
            }
            catch
            {
            }
            if (testAppUserId == userId)
            {
                return BadRequest("You are not able to report your own comment.");
            }

            int testId = -1;
            try
            {
                testId = db.CommentUserReports.Single(x => x.AccomodationId == report.AccomodationIdValue && x.ApplicationUserId == userId && x.CommentId == report.CommentId && x.IsReported == true).CommentId;
            }
            catch
            {
                CommentUserReport newCommentUserReport = new CommentUserReport()
                {
                    AccomodationId = report.AccomodationIdValue,
                    ApplicationUserId = userId,
                    CommentId = report.CommentId,
                    CommentUserReportId = 0,
                    IsReported = true
                };

                db.CommentUserReports.Add(newCommentUserReport);
                db.SaveChanges();

                var userComment = db.UserComments.Find(report.CommentId);
                userComment.ReportCount++;

                db.Entry(userComment).State = EntityState.Modified;
                db.SaveChanges();

                return Ok("Thank you for reporting comment.");
            }

            if (testId != -1 && String.IsNullOrEmpty(userId))
            {
                return BadRequest("Please sign in to report this comment!");
            }
            else if (testId != -1)
            {
                return BadRequest("You have already reported this comment!");
            }

            return Ok();
        }



        //OLD USER COMMENTS ACTIONS - END
        

        // GET: api/UserCommentsAPI
        public IQueryable<UserComment> GetUserComments()
        {
            return db.UserComments;
        }

        

        // PUT: api/UserCommentsAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserComment(int id, UserComment userComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userComment.UserCommentId)
            {
                return BadRequest();
            }

            db.Entry(userComment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCommentExists(id))
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

        // POST: api/UserCommentsAPI
        [ResponseType(typeof(UserComment))]
        public IHttpActionResult PostUserComment(UserComment userComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserComments.Add(userComment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userComment.UserCommentId }, userComment);
        }

        // DELETE: api/UserCommentsAPI/5
        [ResponseType(typeof(UserComment))]
        public IHttpActionResult DeleteUserComment(int id)
        {
            UserComment userComment = db.UserComments.Find(id);
            if (userComment == null)
            {
                return NotFound();
            }

            db.UserComments.Remove(userComment);
            db.SaveChanges();

            return Ok(userComment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserCommentExists(int id)
        {
            return db.UserComments.Count(e => e.UserCommentId == id) > 0;
        }
    }
}