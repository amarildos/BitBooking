using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using BitBooking.API.Controllers;
using BitBooking.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;

namespace BitBookingTest
{
    [TestClass]
    public class RoomPricesControllerTest
    {

        [TestMethod]
        public void RoomPricesIndex()
        {
            RoomPricesController controller = new RoomPricesController();

            var result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }
        [TestMethod]
        public void RoomPricesDetailsNull()
        {
            RoomPricesController controller = new RoomPricesController();
            var result = (HttpStatusCodeResult)controller.Details(null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void RoomPricesDetailsNotFound()
        {
            RoomPricesController controller = new RoomPricesController();
            var result = (HttpStatusCodeResult)controller.Details(-1);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        //[TestMethod]
        //public void RoomPricesDetails()
        //{
        //    RoomPricesController controller = new RoomPricesController();
        //    var result = controller.Details(1);
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));

        //}
        [TestMethod]
        public void RoomPricesCreate()
        {
            RoomPricesController controller = new RoomPricesController();
            RoomPrice roomPrice = new RoomPrice();
            roomPrice.RoomTypeId = 1;
            roomPrice.StartDate = new DateTime(2015, 9, 17);
            roomPrice.EndDate = new DateTime(2015, 9, 25);
            roomPrice.SpecialPrice = 250;
            roomPrice.RoomPriceId = 0;
            roomPrice.AccomodationId = 1;

            var result = controller.Create(roomPrice);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));


       

        }

        [TestMethod]

        public void AccomodationsCreateInvalidInput()
        {
            RoomPricesController controller = new RoomPricesController();
            RoomPrice roomPrice = new RoomPrice();
            roomPrice.RoomTypeId = 1;
            roomPrice.StartDate = new DateTime(2015, 9, 17);
            roomPrice.EndDate = new DateTime(2015, 9, 25);
            roomPrice.SpecialPrice = -250;
            roomPrice.RoomPriceId = 0;
            roomPrice.AccomodationId = 1;

            var context = new ValidationContext(roomPrice, null, null);
            var results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(RoomPrice), typeof(RoomPrice)), typeof(RoomPrice));

            var isModelStateValid = Validator.TryValidateObject(roomPrice, context, results, true);
            Assert.AreEqual(false, isModelStateValid);

        }

        [TestMethod]
        public void RoomPricesCreateValid()
        {
            RoomPricesController controller = new RoomPricesController();
            var result = controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }
        [TestMethod]
        public void RoomPricesDelete()
        {
            RoomPricesController controller = new RoomPricesController();
            var result = controller.Delete(1);
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void RoomPricesDeleteNull()
        {
            RoomPricesController controller = new RoomPricesController();
            var result = (HttpStatusCodeResult)controller.Delete(null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void RoomPricesDeleteNotFound()
        {
            RoomPricesController controller = new RoomPricesController();
            var result = (HttpStatusCodeResult)controller.Delete(-1);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        //[TestMethod]
        //public void RoomPricesDeleteConfirmed()
        //{
        //    RoomPricesController controller = new RoomPricesController();
        //    var result = controller.DeleteConfirmed(1);
        //    Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        //}

        [TestMethod]
        public void RoomPricesEdit()
        {
            RoomPricesController controller = new RoomPricesController();
            RoomPrice roomPrice = new RoomPrice();
            roomPrice.RoomTypeId = 1;
            roomPrice.StartDate = new DateTime(2015, 9, 17);
            roomPrice.EndDate = new DateTime(2015, 9, 25);
            roomPrice.SpecialPrice = 240;
            roomPrice.RoomPriceId = 1;
            roomPrice.AccomodationId = 1;

            var result = controller.Edit(roomPrice);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

        }

        


    }
}