using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitBooking.DAL.Models;
using System.Web.Security;
using System.Threading.Tasks;

namespace BitBooking.MVC.Controllers
{
     [Authorize(Roles = "Administrator, Hotel Manager")]
    public class TokensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tokens
        /// <summary>
        /// Display list of Tokens
        /// </summary>
        /// <returns>View list of Tokens</returns>
        public ActionResult Index()
        {
            return View(db.Tokens.ToList());
        }

        // GET: Tokens/Details/5
        /// <summary>
        /// Display details of Tokens
        /// </summary>
        /// <returns>View Tokens</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Token token = db.Tokens.Find(id);
            if (token == null)
            {
                return HttpNotFound();
            }
            return View(token);
        }

        // GET: Tokens/Create
        /// <summary>
        /// Create Tokens
        /// </summary>
        /// <returns>View Tokens</returns>
        public ActionResult Create(string email)
        {
            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName");
            Token tk = new Token();
            tk.Hash = Convert.ToString(email);
            return View(tk);
        }

        // POST: Tokens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TokenId,Hash,Used,AccomodationId")] Token token)
        {

            string random = Guid.NewGuid().ToString("D");
            
          string accname = db.Accomodations.FirstOrDefault(x => x.AccomodationId == token.AccomodationId).AccomodationName;

          var email = new InvitationEmail
          {
              To = token.Hash,
              UserName = accname,
              Link = random
          };
          
          await email.SendAsync();
          
          token.Hash = random;

            if (ModelState.IsValid)
            {
                db.Tokens.Add(token);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(token);
        }

        // GET: Tokens/Edit/5
        /// <summary>
        /// Edit Tokens
        /// </summary>
        /// <returns>View of Tokens</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Token token = db.Tokens.Find(id);
            if (token == null)
            {
                return HttpNotFound();
            }
            return View(token);
        }

        // POST: Tokens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TokenId,Hash,Used,AccomodationId")] Token token)
        {
            if (ModelState.IsValid)
            {
                db.Entry(token).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(token);
        }

        // GET: Tokens/Delete/5

        /// <summary>
        /// Delete Tokens
        /// </summary>
        /// <returns>View Tokens</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Token token = db.Tokens.Find(id);
            if (token == null)
            {
                return HttpNotFound();
            }
            return View(token);
        }

        // POST: Tokens/Delete/5
        /// <summary>
        /// Confirm delete of Tokens
        /// </summary>
        /// <returns>View of Index page</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Token token = db.Tokens.Find(id);
            db.Tokens.Remove(token);
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
