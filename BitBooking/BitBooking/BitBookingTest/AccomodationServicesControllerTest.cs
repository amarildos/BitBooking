using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitBooking.API.Controllers;
using System.Web.Mvc;
using BitBooking.DAL.Models;

namespace BitBookingTest
{
    [TestClass]
    public class AccomodationServicesControllerTest
    {
        [TestMethod]
        public void AccomodationServicesIndex()
        {
            AccomodationServicesController controller = new AccomodationServicesController();
            var result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void AccServiceDetailsNull()
        {
            AccomodationServicesController controller = new AccomodationServicesController();
            var result = (HttpStatusCodeResult)controller.Details(null);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
            Assert.AreEqual(400, result.StatusCode);

        }
        [TestMethod]
        public void AccServiceDetailsNotFound()
        {
            AccomodationServicesController controller = new AccomodationServicesController();
            var result = (HttpStatusCodeResult)controller.Details(-1);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }

        [TestMethod]
        public void AccServiceDetails()
        {
            AccomodationServicesController controller = new AccomodationServicesController();
            var result = controller.Details(1);
            Assert.IsNotNull(result, "Not Expected View");
            Assert.IsInstanceOfType(result, typeof(object));
        }
        [TestMethod]
        public void AccServiceCreate()
        {
            AccomodationServicesController controller = new AccomodationServicesController();
            AccomodationService accs = new AccomodationService();
            accs.AccomodationId = 1;
            accs.AccomodationServiceId = 1;  
            accs.AccomodationServiceTypeId = 1;
            accs.Name = "BabyPlayground";
   
            var result = controller.Create(accs);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void AccServiceCreateValid()
        {
            AccomodationServicesController controller = new AccomodationServicesController();
            var result = controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }


        [TestMethod]
        public void AccServiceDeleteNull()
        {
            AccomodationServicesController controller = new AccomodationServicesController();
            var result = (HttpStatusCodeResult)controller.Delete(null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }

        [TestMethod]
        public void AccServiceDeleteNotValid()
        {
            AccomodationServicesController controller = new AccomodationServicesController();
            var result = (HttpStatusCodeResult)controller.Delete(-1);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void AccServiceDelete()
        {
            AccomodationServicesController controller = new AccomodationServicesController();
            var result = controller.Delete(1);
            Assert.IsNotNull(result);

        }
        //[TestMethod]
        //public void AccServiceDeleteConfirmed()
        //{
        //    AccomodationServicesController controller = new AccomodationServicesController();
        //    var result = controller.DeleteConfirmed(1);
        //    Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        //}
        [TestMethod]
        public void AccServiceEdit()
        {
            AccomodationServicesController controller = new AccomodationServicesController();
            AccomodationService st = new AccomodationService();
            st.AccomodationId = 1;
            st.AccomodationServiceId = 1;
            st.AccomodationServiceTypeId = 1;
            st.Name = "Baby";
            var result = controller.Edit(st);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.IsTrue(controller.ModelState.IsValid);
        }
        [TestMethod]
        public void AccServiceEditId()
        {
            AccomodationServicesController controller = new AccomodationServicesController();
            var result = (HttpStatusCodeResult)controller.Edit((int?)null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

    }
}
