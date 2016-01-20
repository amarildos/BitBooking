using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitBooking.API.Controllers;
using System.Web.Mvc;
using BitBooking.DAL.Models;
using System.Collections.Generic;
using System.Net;
using System.Data.Entity.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BitBookingTest
{
    [TestClass]
    public class AccomodationsControllerTest
    {
        //[TestMethod]
        //public void AccomodationsIndex()
        //{
        //    AccomodationsController controller = new AccomodationsController();
        //    var result = controller.Index(null, null, null, "testniUser");
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));

        //}

        //[TestMethod]
        //public void AccomodationsIndexWrongDates()
        //{
        //    AccomodationsController controller = new AccomodationsController();
        //    var result = controller.Index(null, new DateTime(2016, 2, 1), new DateTime(2016, 1, 1), "testniUser");
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));

        //}

        //[TestMethod]
        //public void AccomodationsIndexCorrectDates()
        //{
        //    AccomodationsController controller = new AccomodationsController();
        //    var result = controller.Index(null, new DateTime(2016, 1, 1), new DateTime(2016, 2, 1), "testniUser");
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));
        //}

        //[TestMethod]
        //public void AccomodationsIndexCorrectDatesWithSearchDestination()
        //{
        //    AccomodationsController controller = new AccomodationsController();
        //    var result = controller.Index("Dubrovnik", new DateTime(2016, 1, 1), new DateTime(2016, 2, 1), "testniUser");
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));
        //}

        //[TestMethod]
        //public void AccomodationsIndexSearchDestinationWithoutDates()
        //{
        //    AccomodationsController controller = new AccomodationsController();
        //    var result = controller.Index("Dubrovnik", null, null, "testniUser");
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));
        //}

        [TestMethod]
        public void SearchStringCheckTestWithString()
        {
            AccomodationsController controller = new AccomodationsController();
            var result = controller.SearchStringCheck("Dubrovnik");
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void SearchStringCheckTestWithoutString()
        {
            AccomodationsController controller = new AccomodationsController();
            var result = controller.SearchStringCheck(null);
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void CheckSearchDatesLogicTestNormal()
        {
            AccomodationsController controller = new AccomodationsController();
            var result = controller.CheckIfArrivalDateIsAfterDepartueDate(new DateTime(2016, 1, 1), new DateTime(2016, 2, 1));
            Assert.AreEqual(result, true);
        }

        //[TestMethod]
        //public void CheckSearchDatesLogicTestOlderDate()
        //{
        //    AccomodationsController controller = new AccomodationsController();
        //    var result = controller.CheckIfArrivalDateIsAfterDepartueDate(new DateTime(2014, 1, 1), new DateTime(2016, 2, 1));
        //    Assert.AreEqual(result, false);
        //}

        [TestMethod]
        public void CheckSearchDatesLogicTestArrivalLargerThenDeparture()
        {
            AccomodationsController controller = new AccomodationsController();
            var result = controller.CheckIfArrivalDateIsAfterDepartueDate(new DateTime(2017, 1, 1), new DateTime(2016, 2, 1));
            Assert.AreEqual(result, false);
        }


        [TestMethod]
        public void FindAvailableRoomsTest()
        {
            List<Accomodation> testList = new List<Accomodation>();

            AccomodationsController controller = new AccomodationsController();
            var result = controller.FindAvailableRooms(testList, new DateTime(2015, 1, 1), new DateTime(2016, 1, 1),"testniUser");
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        //[TestMethod]
        //public void FindAvailableRoomsTestWithSimpleList()
        //{
        //    List<Accomodation> testList = new List<Accomodation>();
        //    testList.Add(new Accomodation() 
        //    {
        //    AccomodationId = 1,
        //    AccomodationInfoId = 1,
        //    AccomodationName = "Test Accomodation",
        //    AccomodationTypeId = 1,
        //    Description = "Test accomodation desctiption",
        //    NumberOfRooms = 258,
        //    StarRating = 5
        //    });

        //    AccomodationsController controller = new AccomodationsController();
        //    var result = controller.FindAvailableRooms(testList, new DateTime(2015, 1, 1), new DateTime(2016, 1, 1));
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));
        //}

        [TestMethod]
        public void NoHotelsFoundTest()
        {
            AccomodationsController controller = new AccomodationsController();
            var result = controller.NoHotelsFound();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            //ok
        }

        [TestMethod]
        public void NoRoomsFoundTest()
        {
            AccomodationsController controller = new AccomodationsController();
            var result = controller.NoRoomsFound();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            //ok
        }





        [TestMethod]
        public void AccomodationsDetails()
        {
            AccomodationsController controller = new AccomodationsController();

            var result = controller.Details(1);
            Assert.IsNotNull(result, "Not Expected View");
            Assert.IsInstanceOfType(result, typeof(object));
            //ok
        }
        [TestMethod]
        public void AccomodationsDetailsNull()
        {
            AccomodationsController controller = new AccomodationsController();
            var result = (HttpStatusCodeResult)controller.Details(null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
            //ok
        }
        [TestMethod]
        public void AccomodationsDetailsNotFound()
        {
            AccomodationsController controller = new AccomodationsController();
            var result = (HttpStatusCodeResult)controller.Details(-1);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
            //ok
        }


        [TestMethod]

        public void AccomodationsCreate()
        {
            AccomodationsController controller = new AccomodationsController();
            Accomodation acc = new Accomodation();
            acc.AccomodationId = 0;
            acc.AccomodationInfoId = 1;
            acc.AccomodationName = "Test Accomodation";
            acc.AccomodationTypeId = 1;
            acc.Description = "Test accomodation desctiption";
            acc.NumberOfRooms = 999;
            acc.StarRating = 5;

            var result = controller.Create(acc);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

        }
        [TestMethod]
        
        public void AccomodationsCreateInvalidInput()
        {
            AccomodationsController controller = new AccomodationsController();
            Accomodation acc = new Accomodation();
            acc.AccomodationId = 0;
            acc.AccomodationInfoId = 1;
            acc.AccomodationName = "Test Accomodation";
            acc.AccomodationTypeId = 1;
            acc.Description = "Test accomodation desctiption";
            acc.NumberOfRooms = -999;
            acc.StarRating = 5;
           
            var context = new ValidationContext(acc, null, null);
            var results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Accomodation), typeof(Accomodation)), typeof(Accomodation));

            var isModelStateValid = Validator.TryValidateObject(acc, context, results, true);
            Assert.AreEqual(false, isModelStateValid);
            
        }
        [TestMethod]
        public void AccomodationsCreateValid()
        {
            AccomodationsController controller = new AccomodationsController();
            var result = controller.Create() as ViewResult;
            var newResult = controller.Create();
            Assert.IsInstanceOfType(newResult, typeof(ViewResult));
            //ok
        }
        [TestMethod]
        public void AccomodationsDelete()
        {
            AccomodationsController controller = new AccomodationsController();
            var result = controller.Delete(1);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void AccomodationsDeleteNull()
        {
            AccomodationsController controller = new AccomodationsController();

            var result = (HttpStatusCodeResult)controller.Delete(null);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
            Assert.AreEqual(400, result.StatusCode);

        }
        [TestMethod]
        public void AccomodationsDeleteNotFound()
        {
            AccomodationsController controller = new AccomodationsController();
            var result = (HttpStatusCodeResult)controller.Delete(-5);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }
        //[TestMethod]
        //public void AccomodationsDeleteConfirmed()
        //{
        //    AccomodationsController controller = new AccomodationsController();
        //    var result = controller.DeleteConfirmed(2);
        //    Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        //}
        [TestMethod]
        public void AccomodationsEdit()
        {
            AccomodationsController controller = new AccomodationsController();
            var result = (HttpStatusCodeResult)controller.Edit((int?)null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void AccomodationsEditNew()
        {

            AccomodationsController controller = new AccomodationsController();
            Accomodation acc = new Accomodation();
            acc.AccomodationId = 1;
            acc.AccomodationInfoId = 1;
            acc.AccomodationName = "Test Accomodation";
            acc.AccomodationTypeId = 1;
            acc.Description = "Test accomodation desctiption";
            acc.NumberOfRooms = 999;
            acc.StarRating = 4;

            var result = controller.Edit(acc);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            //ok


        }
    }
}
