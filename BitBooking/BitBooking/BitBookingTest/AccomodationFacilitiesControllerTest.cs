using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using BitBooking.API.Controllers;
using BitBooking.DAL.Models;
using System.Threading;

namespace BitBookingTest
{
    [TestClass]
    public class AccomodationFacilitiesControllerTest
    {
        [TestMethod]
        public void AccomodationFacilitiesIndex()
        {
            AccomodationFacilitiesController controller = new AccomodationFacilitiesController();

            var result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }
        [TestMethod]
        public void AccomodationFacilitiesDetailsNull()
        {
            AccomodationFacilitiesController controller = new AccomodationFacilitiesController();
            var result = (HttpStatusCodeResult)controller.Details(null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void AccomodationFacilitiesDetailsNotFound()
        {
            AccomodationFacilitiesController controller = new AccomodationFacilitiesController();
            var result = (HttpStatusCodeResult)controller.Details(-4);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
      
        [TestMethod]
        public void AccomodationFacilitiesDetails()
        {
            AccomodationFacilitiesController controller = new AccomodationFacilitiesController();
            var result = controller.Details(1) as ViewResult;
            Assert.IsNotNull(result, "Not Expected View");
            Assert.IsInstanceOfType(result, typeof(object));
           

        }
        [TestMethod] 
        public void AccomodationFacilitiesCreate()
        {
            AccomodationFacilitiesController controller = new AccomodationFacilitiesController();
            AccomodationFacility fc = new AccomodationFacility();
            fc.AccomodationFacilityId = 0;
            fc.AccomodationId = 1;
            fc.Description = "Testna Facility";
            fc.EndHours = new DateTime(1999, 1, 1,12,0,0);
            fc.StartHours = new DateTime(1999, 1, 1,8,0,0);
            fc.Name = "Ime za facility testni";

            var result = controller.Create(fc);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }



        [TestMethod]
        public void AccomodationFacilitiesCreateValid()
        {
            AccomodationFacilitiesController controller = new AccomodationFacilitiesController();
            var result = controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }
        [TestMethod]
        
        public void AccomodationFacilitiesEdit()
        {
            AccomodationFacilitiesController controller = new AccomodationFacilitiesController();
       
            AccomodationFacility accfacility = new AccomodationFacility();
            accfacility.AccomodationFacilityId = 1;
            accfacility.AccomodationId = 1;
            accfacility.Description = "Testna Verzija";
            accfacility.EndHours = new DateTime(1999, 1, 1, 12, 0, 0);
            accfacility.StartHours = new DateTime(1999, 1, 1, 8, 0, 0);
            accfacility.Name = "Ime";
            var result = controller.Edit(accfacility);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
  
            Assert.IsTrue(controller.ModelState.IsValid);
        }

        


        [TestMethod]
        public void AccomodationFacilitiesEditId()
        {
            AccomodationFacilitiesController controller = new AccomodationFacilitiesController();
            var result = (HttpStatusCodeResult)controller.Edit((int?)null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

       [TestMethod]
        public void AccomodationFacilitiesDelete()
        {
            AccomodationFacilitiesController controller = new AccomodationFacilitiesController();
            var result = controller.Delete(4);
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void AccomodationFacilitiesDeleteNull()
        {
            AccomodationFacilitiesController controller = new AccomodationFacilitiesController();
            var result = (HttpStatusCodeResult)controller.Delete(null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void AccomodationFacilitiesDeleteNotFound()
        {
            AccomodationFacilitiesController controller = new AccomodationFacilitiesController();
            var result = (HttpStatusCodeResult)controller.Delete(-3);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }
        //[TestMethod]
        //public void AccomodationFacilitiesDeleteConfirmed()
        //{

        //    AccomodationFacilitiesController controller = new AccomodationFacilitiesController();
        //    var result = controller.DeleteConfirmed(2);
        //    Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        //}
   

    }
}
