using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using BitBooking.API.Controllers;
using BitBooking.DAL.Models;


namespace BitBookingTest
{
    [TestClass]
    public class AccomodationTypesControllerTest
    {

        [TestMethod]
        public void AccomodationTypesIndex()
        {
            AccomodationTypesController controller = new AccomodationTypesController();

            var result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }
        [TestMethod]
        public void AccomodationTypessDetailsNull()
        {
            AccomodationTypesController controller = new AccomodationTypesController();
            var result = (HttpStatusCodeResult)controller.Details(null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void AccomodationTypesDetailsNotFound()
        {
            AccomodationTypesController controller = new AccomodationTypesController();
            var result = (HttpStatusCodeResult)controller.Details(-1);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void AccomodationTypesDetails()
        {
            AccomodationTypesController controller = new AccomodationTypesController();
            var result = controller.Details(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }
        [TestMethod]
        public void AccomodationTypesCreate()
        {
            AccomodationTypesController controller = new AccomodationTypesController();
            AccomodationType accType = new AccomodationType();
            accType.AccomodationTypeId = 0;
            accType.AccomodationTypeName = "Tent";
            var result = controller.Create(accType);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void AccomodationTypesCreateValid()
        {
            AccomodationTypesController controller = new AccomodationTypesController();
            var result = controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }
        [TestMethod]
        public void AccomodationTypesDelete()
        {
            AccomodationTypesController controller = new AccomodationTypesController();
            var result = controller.Delete(1);
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void AccomodationTypesDeleteNull()
        {
            AccomodationTypesController controller = new AccomodationTypesController();
            var result = (HttpStatusCodeResult)controller.Delete(null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void AccomodationTypesDeleteNotFound()
        {
            AccomodationTypesController controller = new AccomodationTypesController();
            var result = (HttpStatusCodeResult)controller.Delete(-1);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }
    
        //[TestMethod]
        //public void AccomodationTypesDeleteConfirmed()
        //{
        //    AccomodationTypesController controller = new AccomodationTypesController();
        //    var result = controller.DeleteConfirmed(1);
        //    Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        //}

        [TestMethod]
        public void AccomodationTypesEdit()
        {
            AccomodationTypesController controller = new AccomodationTypesController();
            AccomodationType accType = new AccomodationType();
            accType.AccomodationTypeId = 1;
            accType.AccomodationTypeName = "Apartment";
            var result = controller.Edit(accType);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            

        }


       

        }
    }
