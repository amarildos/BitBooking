using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitBooking.API.Controllers;
using System.Web.Mvc;
using BitBooking.DAL.Models;

namespace BitBookingTest
{
    [TestClass]
    public class AccomodationServiceTypesControllerTest
    {
        [TestMethod]
        public void AccServiceTypesIndex()
        {
            AccomodationServiceTypesController controller = new AccomodationServiceTypesController();
            var result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void AccServiceTypesDetailsNull()
        {
            AccomodationServiceTypesController controller = new AccomodationServiceTypesController();
            var result = (HttpStatusCodeResult)controller.Details(null);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
            Assert.AreEqual(400, result.StatusCode);

        }
        [TestMethod]
        public void AccServiceTypesDetailsNotFound()
        {
            AccomodationServiceTypesController controller = new AccomodationServiceTypesController();
            var result = (HttpStatusCodeResult)controller.Details(-1);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }

        [TestMethod]
        public void AccServiceTypesDetails()
        {
            AccomodationServiceTypesController controller = new AccomodationServiceTypesController();
            var result = controller.Details(1);
            Assert.IsNotNull(result, "Not Expected View");
            Assert.IsInstanceOfType(result, typeof(object));
        }
        [TestMethod]
        public void AccServiceTypesCreate()
        {
            AccomodationServiceTypesController controller = new AccomodationServiceTypesController();
            AccomodationServiceType st = new AccomodationServiceType();
            st.AccomodationServiceTypeId=1;
            st.Name="BabySitter";
            var result= controller.Create(st);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void AccServiceTypesCreateValid()
        {
            AccomodationServiceTypesController controller = new AccomodationServiceTypesController();
            var result = controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void AccServiceTypesDeleteNull()
        {
            AccomodationServiceTypesController controller = new AccomodationServiceTypesController();
            var result = (HttpStatusCodeResult)controller.Delete(null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult)); 
        
        }

        [TestMethod]
        public void AccServiceTypesDeleteNotValid()
        {
            AccomodationServiceTypesController controller = new AccomodationServiceTypesController();
            var result = (HttpStatusCodeResult)controller.Delete(-1);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void AccServiceTypesDelete()
        {
            AccomodationServiceTypesController controller = new AccomodationServiceTypesController();
            var result = controller.Delete(1);
            Assert.IsNotNull(result);

        }
        //[TestMethod]
        //public void AccServiceTypesDeleteConfirmed()
        //{
        //    AccomodationServiceTypesController controller = new AccomodationServiceTypesController();
        //    var result = controller.DeleteConfirmed(4);
        //    Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        //}
       
        [TestMethod]
        public void AccServiceTypesEditId()
        {
            AccomodationServiceTypesController controller = new AccomodationServiceTypesController();
            var result = (HttpStatusCodeResult)controller.Edit((int?)null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }
        [TestMethod]
        public void AccServiceTypesEdit()
        {
            AccomodationServiceTypesController controller = new AccomodationServiceTypesController();
            AccomodationServiceType st = new AccomodationServiceType();
            st.AccomodationServiceTypeId = 1;
            st.Name = "PlayGround";
            var result = controller.Edit(st);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.IsTrue(controller.ModelState.IsValid);
        }

    }
}
