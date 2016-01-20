using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitBooking.API.Controllers;
using System.Web.Mvc;
using BitBooking.DAL.Models;

namespace BitBookingTest
{
    [TestClass]
    public class RoomTypesControllerTest
    {
        [TestMethod]
        public void RoomTypesIndex()
        {
            RoomTypesController controller = new RoomTypesController();
            var result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void RoomTypesDetails()
        {
            RoomTypesController controller = new RoomTypesController();

            var result = controller.Details(1);
            Assert.IsNotNull(result, "Not Expected View");
            Assert.IsInstanceOfType(result, typeof(object));

        }
        [TestMethod]
        public void RoomTypesDetailsNull ()
        {
            RoomTypesController controller = new RoomTypesController();
            var result = (HttpStatusCodeResult)controller.Details(null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void RoomTypesDetailsNotFound()
        {
            RoomTypesController controller = new RoomTypesController();
            var result = (HttpStatusCodeResult)controller.Details(-1);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }
        [TestMethod]
        public void RoomTypesCreate()
        {
            RoomTypesController controller = new RoomTypesController();
            RoomType rt = new RoomType();
            rt.RoomTypeId = 0;
            rt.RoomTypeName="Deluxe";
            var result = controller.Create(rt);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }
        [TestMethod]
        public void RoomTypesCreateValid()
        {
            RoomTypesController controller = new RoomTypesController();
            var result = controller.Create() as ViewResult;
            Assert.IsNotNull("...", result.ViewName);
            var newResult = controller.Create();
            Assert.IsInstanceOfType(newResult, typeof(ViewResult));
        }
        [TestMethod]
        public void RoomTypeDelete()
        {
            RoomTypesController controller = new RoomTypesController();
            var result = controller.Delete(1);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void RoomTypesDeleteNull()
        {
            RoomTypesController controller = new RoomTypesController();

            var result = (HttpStatusCodeResult)controller.Delete(null);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
            Assert.AreEqual(400, result.StatusCode);

        }
        [TestMethod]
        public void RoomTypesDeleteNotFound()
        {
            RoomTypesController controller = new RoomTypesController();
            var result = (HttpStatusCodeResult)controller.Delete(-5);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }
        //[TestMethod]
        //public void RoomTypesDeleteConfirmed()
        //{
        //    RoomTypesController controller = new RoomTypesController();
        //    var result = controller.DeleteConfirmed(2);
        //    Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        //}
        [TestMethod]
        public void RoomTypesEdit()
        {
            RoomTypesController controller = new RoomTypesController();
            var result = (HttpStatusCodeResult)controller.Edit((int?)null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void RoomTypeEdit()
        {

             RoomTypesController controller = new RoomTypesController();
             RoomType rt = new RoomType();

             rt.RoomTypeId = 1;
             rt.RoomTypeName = "Premier Room with One King Bed ";
             var result = controller.Edit(rt);
             Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
             Assert.IsTrue(controller.ModelState.IsValid);
       

        }

    }
}
