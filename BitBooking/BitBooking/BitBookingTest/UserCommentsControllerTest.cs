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
    public class UserCommentsControllerTest
    {
        [TestMethod]
        public void UserCommentsIndex()
        {
            UserCommentsController controller = new UserCommentsController();
            var result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }
     
        [TestMethod]
        public void UserCommentsDetails()
        {
            UserCommentsController controller = new UserCommentsController();
            var result = (HttpStatusCodeResult)controller.Details(null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public void UserCommentsDetailsNew()
        {
            UserCommentsController controller = new UserCommentsController();
            var result = (HttpStatusCodeResult)controller.Details(-1);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public void UserComments()
        {
            UserCommentsController controller = new UserCommentsController();
            var result = controller.Details(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(object));
        }
        //[TestMethod]
        //public void UserCommentsCreate()
        //{
        //    UserCommentsController controller = new UserCommentsController();
        //    UserComment  uc = new UserComment();
        //    uc.UserCommentId = 1;
        //    uc.AccomodationId = 1;
        //    uc.Comment = "Some new comment";
        //    uc.ApplicationUserId = "12345";
        //    uc.UserName = "My Username";
        //    uc.Rating = 2;
        //    uc.ReportCount=5;

        //    var result = controller.Create(uc);
        //    Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
           
        //}

        [TestMethod]

        public void UserCommentsCreateInvalidInput()
        {
            UserCommentsController controller = new UserCommentsController();
            UserComment uc = new UserComment();
            uc.UserCommentId = 1;
            uc.AccomodationId = 1;
            uc.Comment = "Some new comment";
            uc.ApplicationUserId = "12345";
            uc.UserName = "My Username";
            uc.Rating = -2;
            uc.ReportCount = -5;


            var context = new ValidationContext(uc, null, null);
            var results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(UserComment), typeof(UserComment)), typeof(UserComment));

            var isModelStateValid = Validator.TryValidateObject(uc, context, results, true);
            Assert.AreEqual(false, isModelStateValid);

        }


        [TestMethod]
        public void CreateValidate()
        {
            UserCommentsController controller = new UserCommentsController();
            var result = controller.Create() as ViewResult;
            var newResult = controller.Create();
            Assert.IsInstanceOfType(newResult, typeof(ViewResult));
        }
        //[TestMethod]
        //public void UserCommentsEdit()
        //{
        //    UserCommentsController controller = new UserCommentsController();
        //    UserComment uc = new UserComment();
        //    uc.UserCommentId = 1;
        //    uc.AccomodationId = 1;
        //    uc.Comment = "Totaly new comment";
        //    uc.ApplicationUserId = "12345";
        //    uc.UserName = "My Username";
        //    uc.Rating = 2;
        //    uc.ReportCount = 5;

        //    var result = controller.Edit(uc);
        //    Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        //    Assert.IsTrue(controller.ModelState.IsValid);
            

        //}
        [TestMethod]
        public void UserCommentsEditId()
        {
            UserCommentsController controller = new UserCommentsController();
            var result = (HttpStatusCodeResult)controller.Edit((int?)null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
        [TestMethod]
        public void UserCommentsDeleteNull()
        {
            UserCommentsController controller = new UserCommentsController();
            var result = (HttpStatusCodeResult)controller.Delete(null);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }
         [TestMethod]
        public void UserCommentsDeleteNotFount()
        {
            UserCommentsController controller = new UserCommentsController();
            var result = (HttpStatusCodeResult)controller.Delete(-1);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));

        }
         [TestMethod]
         public void UserCommentsDelete()
         {
             UserCommentsController controller = new UserCommentsController();
             var result = controller.Delete(1);
             Assert.IsNotNull(result);
         }
         //[TestMethod]
         //public void DeleteConfirmed()
         //{
         //    UserCommentsController controller = new UserCommentsController();
         //    var result = controller.DeleteConfirmed(2);
         //    Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
         //}
        }
    }

