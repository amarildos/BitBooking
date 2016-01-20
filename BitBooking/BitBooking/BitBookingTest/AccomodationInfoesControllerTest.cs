using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitBooking.API.Controllers;
using System.Web.Mvc;
using BitBooking.DAL.Models;

namespace BitBookingTest
{
    [TestClass]
    public class AccomodationInfoesControllerTest
    {
        [TestMethod]
        public void AccomodationInfoesIndex()
        {
            AccomodationInfoesController controller = new AccomodationInfoesController();

            var result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }
        [TestMethod]
        public void AccomodationInfoesDetailsNull()
        {
            AccomodationInfoesController controller = new AccomodationInfoesController();
            var result = (HttpStatusCodeResult)controller.Details(null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void AccomodationInfoesDetailsNotFound()
        {
            AccomodationInfoesController controller = new AccomodationInfoesController();
            var result = (HttpStatusCodeResult)controller.Details(-1);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void AccomodationInfoesDetails()
        {
            AccomodationInfoesController controller = new AccomodationInfoesController();
            var result = controller.Details(1);
            Assert.IsNotNull(result, "Not Expected View");
            Assert.IsInstanceOfType(result, typeof(object));

        }
        [TestMethod]
        public void AccomodationInfoesCreate()
        {
            AccomodationInfoesController controller = new AccomodationInfoesController();
            AccomodationInfo ai = new AccomodationInfo();
            ai.AccomodationId = 1;
            ai.AccomodationInfoId = 0;
            ai.Address = "TMP9";
            ai.City = "Sarajevo";
            ai.Country = "BiH";
            ai.Email = "hotel@bitbooking.ba";
            ai.Phone = "387/454+545";
            ai.PostalCode = "71000";
            var result = controller.Create(ai);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

        }
        [TestMethod]
        public void AccomodationInfoesCreateValid()
        {
            AccomodationInfoesController controller = new AccomodationInfoesController();
            var result = controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }
        [TestMethod]
        public void AccomodationInfoesDelete()
        {
            AccomodationInfoesController controller = new AccomodationInfoesController();
            var result = controller.Delete(3);
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void AccomodationInfoesDeleteNull()
        {
            AccomodationInfoesController controller = new AccomodationInfoesController();
            var result = (HttpStatusCodeResult)controller.Delete(null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void AccomodationInfoesDeleteNotFound()
        {
            AccomodationInfoesController controller = new AccomodationInfoesController();
            var result = (HttpStatusCodeResult)controller.Delete(-1);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }
      
        [TestMethod]
        public void AccomodationInfoesEdit()
        {
            AccomodationInfoesController controller = new AccomodationInfoesController();
            
            AccomodationInfo ai = new AccomodationInfo();
            ai.AccomodationId = 3;
            ai.AccomodationInfoId = 3;
            ai.Address = "TMP9";
            ai.City = "Pariz";
            ai.Country = "BiH";
            ai.Email = "hotel@bitbooking.ba";
            ai.Phone = "387/454+545";
            ai.PostalCode = "71000";
            var result = controller.Edit(ai);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            
            Assert.IsTrue(controller.ModelState.IsValid);

        }


        [TestMethod]
        public void AccomodationInfoesEditId()
        {
            AccomodationFacilitiesController controller = new AccomodationFacilitiesController();
            var result = (HttpStatusCodeResult)controller.Edit((int?)null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        //[TestMethod]
        //public void AccomodationInfoesDeleteConfirmed()
        //{
        //    AccomodationInfoesController controller = new AccomodationInfoesController();
        //    var result = controller.DeleteConfirmed(3);
        //    Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        //}
    }
}