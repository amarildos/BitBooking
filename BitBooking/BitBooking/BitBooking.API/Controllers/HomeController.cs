using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using BitBooking.DAL.Models;
using System.Data.Entity;


namespace BitBooking.API.Controllers
{
      
    public class HomeController : Controller 
    {
        private ApplicationDbContext db = new ApplicationDbContext();
   

        public ActionResult Index(int? reserved)
        {
            if (reserved== 1)
            { ViewBag.reservation = "You have successfully reserved room!"; }
            ViewBag.nejm = this.User.Identity.GetUserName();
           var keys= this.Request.Headers.Keys;
           // HttpRequest req = new HttpRequest();
            var kijs=Session.Keys;
            int a= HttpContext.Session.Count;
            ValuesController value = new ValuesController();
         
           // value.RequestContext.Principal.Identity.Name;
            ClaimsIdentity cl = (ClaimsIdentity)User.Identity;
           // string username = cl.Claims.First().Value;
            string username="";
            try { username = value.RequestContext.Principal.Identity.Name; }

            catch { }
          //  ApplicationUserManager pr = new ApplicationUserManager();
         
        
    
    ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult IndexTest()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

     
        public ActionResult Menu()
        {

           var currentusername=this.User.Identity.GetUserName();
           if (currentusername == "")
               return HttpNotFound();

           return View();



        
        }
        public ActionResult Error()
        {

            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(string name, string lastname, string phone, string county)
        {

            db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;

            var user = db.Users.FirstOrDefault(x => x.Id == userId);
            user.LastName = lastname;
            user.FirstName = name;
            user.Country = county;
            user.Phone = phone;

            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();


         

            return Json("Ok");




        }

        public ActionResult ProfileInfo()
        {

            var tempId=User.Identity.GetUserId();




            return Json(db.Users.Where(x => x.Id == tempId).Select(x => new { x.LastName, x.FirstName, x.Phone, x.Country}) ,JsonRequestBehavior.AllowGet);




        }

        
        [Authorize]
        public ActionResult GetInfo()
        { 
        
           db.Configuration.ProxyCreationEnabled = false;
            var userId = this.User.Identity.GetUserId();
            int? accID = db.Users.FirstOrDefault(x => x.Id == userId).AccomodationId;

            var user = db.Users.Select(x=> new{x.Id,x.Phone, x.FirstName, x.LastName, x.Country}).FirstOrDefault(x => x.Id== userId);
        
        return Json(user, JsonRequestBehavior.AllowGet);
        }



        public ActionResult ProfilePage()
        {
            var currentusername = this.User.Identity.GetUserName();
            if (currentusername == "")
                return HttpNotFound();

          var first=  db.Users.FirstOrDefault(x => x.UserName == currentusername);

            return View(first);

        }
    }
}
