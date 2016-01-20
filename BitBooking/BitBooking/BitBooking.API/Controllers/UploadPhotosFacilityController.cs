using BitBooking.DAL.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace BitBooking.API.Controllers
{
    public class UploadPhotosFacilityController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [System.Web.Http.HttpPost]
        public void UploadFile(string facilityId, string AccomodationId)
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var httpPostedFile = HttpContext.Current.Request.Files["file"];
                    bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/UploadedDocuments"));
                    if (!folderExists)
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UploadedDocuments"));
                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedDocuments"),
                                                    httpPostedFile.FileName);
                    httpPostedFile.SaveAs(fileSavePath);

                    if (File.Exists(fileSavePath))
                    {
                        Account account = new Account("bitbooking", "131162311141994", "yqy4VSrjuxaGeP8BUMgHwTozpfw");
                        Cloudinary cloudinary = new Cloudinary(account);

                        var uploadParams = new ImageUploadParams()  //upload to cloudinary
                        {
                            File = new FileDescription(fileSavePath),
                            Transformation = new Transformation().Crop("fill").Width(720).Height(480)

                        };

                        var uploadResult = cloudinary.Upload(uploadParams);
                        System.IO.File.Delete(fileSavePath);

                        try
                        {
                            int accomodationID = 0;
                            int newFacilityId = 0;
                            bool parseSuccedAcc = Int32.TryParse(AccomodationId, out accomodationID);
                            bool parseSuccedFacility = Int32.TryParse(facilityId, out newFacilityId);
                            if (parseSuccedAcc == true & parseSuccedFacility == true)
                            {
                                string photoUrl = "http://res.cloudinary.com" + uploadResult.Uri.AbsolutePath;
                                Photo newPhoto = new Photo();
                                newPhoto.AccomodationId = accomodationID;
                                newPhoto.PhotoUrl = photoUrl;
                                newPhoto.RoomTypeId = 1;
                                newPhoto.FacilityId = newFacilityId;
                                newPhoto.Priority = 99;
                                db.Images.Add(newPhoto);
                                db.SaveChanges();

                                try
                                {
                                    var facility = db.AccomodationFacilities.Single(x => x.AccomodationFacilityId == newFacilityId);
                                    facility.PhotoUrl = photoUrl;
                                    db.Entry(facility).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                                catch(Exception e)
                                {

                                }
                            }
                        }
                        catch (Exception e)
                        {

                        }

                    }


                    //// http://www.codeproject.com/Tips/900200/SFTP-File-Upload-Using-ASP-NET-Web-API-and-Angular

                }
            }
        }
    }
}
