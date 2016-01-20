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
    public class RoomsControllerTest
    {
        [TestMethod]
        public void RoomIndex()
        {
            RoomsController controller = new RoomsController();
            var result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }
     
        [TestMethod]
        public void RoomDetails()
        {
            RoomsController controller = new RoomsController();
            var result = (HttpStatusCodeResult)controller.Details(null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public void RoomDetailsNew()
        {
            RoomsController controller = new RoomsController();
            var result = (HttpStatusCodeResult)controller.Details(-1);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public void Rooms()
        {
            RoomsController controller = new RoomsController();
            var result = controller.Details(1);
            Assert.IsNotNull(result, "Not Expected View");
            Assert.IsInstanceOfType(result, typeof(object));
        }
        [TestMethod]
        public void RoomsCreate()
        {
            RoomsController controller = new RoomsController();
            Room rm = new Room();
            rm.AccomodationId = 1;
            rm.NumberOfRooms = 23;
            rm.Price = 50;
            rm.TempReservationString = "reserved";
            rm.RoomCapacity = 2;
            rm.RoomId = 0;  
            rm.RoomTypeId = 1;
            rm.RoomDetails = " Air conditioning, Desk, Heating, Shower, Hairdryer,Flat-screen TV, Wi-Fi,Pay-per-view channels,Minibar";
            var result = controller.Create(rm);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
           
        }

        [TestMethod]

        public void RoomCreateInvalidInput()
        {
            RoomsController controller = new RoomsController();
            Room rm = new Room();
            rm.AccomodationId = 1;
            rm.NumberOfRooms = -23;
            rm.Price = -50;
            rm.TempReservationString = "reserved";
            rm.RoomCapacity = -2;
            rm.RoomId = 0;
            rm.RoomTypeId = 1;
            rm.RoomDetails = " Air conditioning, Desk, Heating, Shower, Hairdryer,Flat-screen TV, Wi-Fi,Pay-per-view channels,Minibar";


            var context = new ValidationContext(rm, null, null);
            var results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Room), typeof(Room)), typeof(Room));

            var isModelStateValid = Validator.TryValidateObject(rm, context, results, true);
            Assert.AreEqual(false, isModelStateValid);

        }

        [TestMethod]
        public void CreateValidate()
        {
            RoomsController controller = new RoomsController();
            var result = controller.Create() as ViewResult;
            var newResult = controller.Create();
            Assert.IsInstanceOfType(newResult, typeof(ViewResult));
        }
        [TestMethod]
        public void RoomsEdit()
        {
            RoomsController controller = new RoomsController();
          
            Room rm = new Room();
            rm.AccomodationId = 1;
            rm.NumberOfRooms = 23;
            rm.Price = 80;
            rm.TempReservationString = "reserved";
            rm.RoomCapacity = 2;
            rm.RoomId = 1;
            rm.RoomTypeId = 1;
            rm.RoomDetails = " Air conditioning, Heating, Shower, Hairdryer,Flat-screen TV, Wi-Fi,Pay-per-view channels,Minibar";

            var result = controller.Edit(rm);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.IsTrue(controller.ModelState.IsValid);
            

        }
        [TestMethod]
        public void RoomsEditId()
        {
            RoomsController controller = new RoomsController();
            var result = (HttpStatusCodeResult)controller.Edit((int?)null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void RoomsDeleteNull()
        {
            RoomsController controller = new RoomsController();
            var result = (HttpStatusCodeResult)controller.Delete(null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }
         [TestMethod]
        public void RoomsDeleteNotFount()
        {
            RoomsController controller = new RoomsController();
            var result = (HttpStatusCodeResult)controller.Delete(-1);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
         [TestMethod]
         public void RoomsDelete()
         {
             RoomsController controller = new RoomsController();
             var result = controller.Delete(1);
             Assert.IsNotNull(result);
         }
         //[TestMethod]
         //public void DeleteConfirmed()
         //{
         //    RoomsController controller = new RoomsController();
         //    var result = controller.DeleteConfirmed(1);
         //    Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
         //}
    }
}
