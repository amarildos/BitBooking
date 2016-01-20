using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using BitBooking.API.Controllers;
using BitBooking.DAL.Models;


namespace BitBookingTest
{
    
        [TestClass]
        public class PhotosControllerTest
        {

            [TestMethod]
            public void PhotosIndex()
            {
                PhotosController controller = new PhotosController();

                var result = controller.Index();
                Assert.IsInstanceOfType(result, typeof(ViewResult));

            }
            [TestMethod]
            public void PhotosDetailsNull()
            {
                PhotosController controller = new PhotosController();
                var result = (HttpStatusCodeResult)controller.Details(null);
                Assert.AreEqual(400, result.StatusCode);
                Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

            }
            [TestMethod]
            public void PhotosDetailsNotFound()
            {
                PhotosController controller = new PhotosController();
                var result = (HttpStatusCodeResult)controller.Details(-1);
                Assert.AreEqual(404, result.StatusCode);
                Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

            }
            [TestMethod]
            public void PhotosDetails()
            {
                PhotosController controller = new PhotosController();
                var result = controller.Details(1);
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(ViewResult));

            }
            [TestMethod]
            public void PhotosCreate()
            {
                PhotosController controller = new PhotosController();
                var result = controller.Create();

                Assert.IsNotNull(result);

            }
            [TestMethod]
            public void PhotosCreateValid()
            {
                PhotosController controller = new PhotosController();
                var result = controller.Create();
                Assert.IsInstanceOfType(result, typeof(ViewResult));

            }
            [TestMethod]
            public void PhotosDelete()
            {
                PhotosController controller = new PhotosController();
                var result = controller.Delete(1);
                Assert.IsNotNull(result);

            }
            [TestMethod]
            public void PhotosDeleteNull()
            {
                PhotosController controller = new PhotosController();
                var result = (HttpStatusCodeResult)controller.Delete(null);
                Assert.AreEqual(400, result.StatusCode);
                Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

            }
            [TestMethod]
            public void PhotosDeleteNotFound()
            {
                PhotosController controller = new PhotosController();
                var result = (HttpStatusCodeResult)controller.Delete(-1);
                Assert.AreEqual(404, result.StatusCode);
                Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
            }

            //[TestMethod]
            //public void PhotosDeleteConfirmed()
            //{
            //    PhotosController controller = new PhotosController();
            //    var result = controller.DeleteConfirmed(27);
            //    Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            //}

            [TestMethod]
            public void PhotosEdit()
            {

                
                PhotosController controller = new PhotosController();
                Photo photoInfo = new Photo();
                photoInfo.PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/bo_2px_solid_rgb:202020,r_5/v1441716099/2_barfns.jpg";
                photoInfo.AccomodationId = 1;
                photoInfo.PhotoId = 1;
                photoInfo.Priority = 1;
                photoInfo.RoomTypeId = 1;
                var result = controller.Edit(photoInfo);
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
                

            }

           

           

        }
    }

