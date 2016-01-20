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
    public class RoomAvailabilitiesControllerTest
    {

        //[TestMethod]
        //public void RoomAvailabilityIndex()
        //{
        //    RoomAvailabilitiesController controller = new RoomAvailabilitiesController();

        //    var result = controller.Index();
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));

        //}
        [TestMethod]
        public void RoomAvailabilityCreate()
        {
            RoomAvailabilitiesController controller = new RoomAvailabilitiesController();
            RoomAvailability roomAvail = new RoomAvailability();
            roomAvail.AccomodationId = 1;
            roomAvail.ArrivalDate = new DateTime(2015, 9, 22);
            roomAvail.DepartureDate = new DateTime(2016, 9, 28);
            roomAvail.IsPaid = true;
            roomAvail.RoomAvailabilityId = 0;
            roomAvail.RoomId = 1;
            roomAvail.TotalPrice = 100;
            roomAvail.UserId = "1";

            var result = controller.Create(roomAvail);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.IsNotNull(result);

        }

        [TestMethod]

        public void RoomAvailabilityInvalidInput()
        {
            RoomAvailabilitiesController controller = new RoomAvailabilitiesController();
            RoomAvailability roomAvail = new RoomAvailability();
            roomAvail.AccomodationId = 1;
            roomAvail.ArrivalDate = new DateTime(2015, 9, 22);
            roomAvail.DepartureDate = new DateTime(2016, 9, 28);
            roomAvail.IsPaid = true;
            roomAvail.RoomAvailabilityId = 0;
            roomAvail.RoomId = 1;
            roomAvail.TotalPrice = -100;
            roomAvail.UserId = "1";


            var context = new ValidationContext(roomAvail, null, null);
            var results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(RoomAvailability), typeof(RoomAvailability)), typeof(RoomAvailability));

            var isModelStateValid = Validator.TryValidateObject(roomAvail, context, results, true);
            Assert.AreEqual(false, isModelStateValid);

        }
        [TestMethod]
        public void RoomAvailabilityCreateValid()
        {
            RoomAvailabilitiesController controller = new RoomAvailabilitiesController();
            var result = controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }
        [TestMethod]
        public void RoomAvailabilityDetailsNull()
        {
            RoomAvailabilitiesController controller = new RoomAvailabilitiesController();
            var result = (HttpStatusCodeResult)controller.Details(null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void RoomAvailabilityDetailsNotFound()
        {
            RoomAvailabilitiesController controller = new RoomAvailabilitiesController();
            var result = (HttpStatusCodeResult)controller.Details(-1);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void RoomAvailabilityDetails()
        {
            RoomAvailabilitiesController controller = new RoomAvailabilitiesController();
            var result = controller.Details(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }
        
        [TestMethod]
        public void RoomAvailabilityDelete()
        {
            RoomAvailabilitiesController controller = new RoomAvailabilitiesController();
            var result = controller.Delete(1);
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void RoomAvailabilityDeleteNull()
        {
            RoomAvailabilitiesController controller = new RoomAvailabilitiesController();
            var result = (HttpStatusCodeResult)controller.Delete(null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void RoomAvailabilitysDeleteNotFound()
        {
            RoomAvailabilitiesController controller = new RoomAvailabilitiesController();
            var result = (HttpStatusCodeResult)controller.Delete(-1);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        //[TestMethod]
        //public void RoomAvailabilityDeleteConfirmed()
        //{
        //    RoomAvailabilitiesController controller = new RoomAvailabilitiesController();
        //    var result = controller.DeleteConfirmed(1);
        //    Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        //}

        [TestMethod]
        public void RoomAvailabilityEdit()
        {
            RoomAvailabilitiesController controller = new RoomAvailabilitiesController();
            RoomAvailability roomAvail = new RoomAvailability();
            roomAvail.AccomodationId = 1;
            roomAvail.ArrivalDate = new DateTime(2015, 9, 22);
            roomAvail.DepartureDate = new DateTime(2016, 9, 28);
            roomAvail.IsPaid = true;
            roomAvail.RoomAvailabilityId = 1;
            roomAvail.RoomId = 1;
            roomAvail.TotalPrice = 1000;
            roomAvail.UserId = "1";

            var result = controller.Edit(roomAvail);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));



        }




    }
}

