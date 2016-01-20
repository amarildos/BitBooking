using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitBooking.DAL.Models;
using System.Drawing;
using System.IO;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNet.Identity;

namespace BitBooking.API.Controllers
{
    public class PhotosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Photos
        public ActionResult Index()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;


            var images = db.Images.Where(x => x.AccomodationId == (int)accID).ToList() ;
            return Json(images, JsonRequestBehavior.AllowGet);
        }

        // GET: Photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Images.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: Photos/Create
        public ActionResult Create()
        {
            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName");
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName");
            return View();
        }

        // POST: Photos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhotoId,PhotoUrl,AccomodationId,RoomTypeId,Priority")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                photo.PhotoUrl = "http://res.cloudinary.com" + HandleFileUpload(ref photo);

                if (photo.PhotoUrl != "http://res.cloudinary.comnone")
                {
                    
                    db.Images.Add(photo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName", photo.AccomodationId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName", photo.RoomTypeId);
            return View(photo);
        }

        private string[] _allowedTypes = new string[] { "image/jpeg", "image/jpg", "image/png" };
        private string HandleFileUpload(ref Photo photo)
        {
            string filePath = @"~\Images\defaultAccomodationPhoto.jpg";
            
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                if (file.ContentLength > 0 && _allowedTypes.Contains(file.ContentType))
                {
                    try
                    {
                        using (var bitmap = new Bitmap(file.InputStream))
                        {
                        }
                    }
                    catch
                    {
                        ModelState.AddModelError("PhotoUrl", "The file type is not supported");
                        return "none";
                    }

                    string fileName = Path.GetFileName(file.FileName);
                    filePath = Path.Combine(@"~\Images\Photos", fileName);
                    string fullPath = Path.Combine(Server.MapPath(@"~\Images\Photos"), fileName);
                    file.SaveAs(fullPath);

                    Account account = new Account(
                                  "bitbooking",
                                  "131162311141994",
                                  "yqy4VSrjuxaGeP8BUMgHwTozpfw");

                    Cloudinary cloudinary = new Cloudinary(account);

                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(fullPath)
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);

                    FileInfo uploadedFileToServer = new FileInfo(fullPath);
                    uploadedFileToServer.Delete();

                    return uploadResult.Uri.AbsolutePath; //new photo URL from Cloudinary
                }
                else
                {
                    if (file.ContentLength > 0 && !_allowedTypes.Contains(file.ContentType))
                    {
                        ModelState.AddModelError("PhotoUrl", "The file type is not supported");
                        return "none";
                    }

                }
            }
            //photo.PhotoUrl = filePath;
            return filePath;
        }



        // GET: Photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Images.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName", photo.AccomodationId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName", photo.RoomTypeId);
            return View(photo);
        }

        // POST: Photos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhotoId,PhotoUrl,AccomodationId,RoomTypeId,Priority")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccomodationId = new SelectList(db.Accomodations, "AccomodationId", "AccomodationName", photo.AccomodationId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName", photo.RoomTypeId);
            return View(photo);
        }

        // GET: Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Images.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;

            Photo photo = db.Images.Find(id);
            if (photo == null||photo.AccomodationId!=accID)
            {
                return HttpNotFound();
            }
            db.Images.Remove(photo);
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
