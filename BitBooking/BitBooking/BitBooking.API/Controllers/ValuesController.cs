using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace BitBooking.API.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public bool Get()
        {
          
            //IPrincipal principal = this.request.GetUserPrincipal();
            //IIdentity identity = principal.Identity;

           
           
         

            return User.IsInRole("Seller");

           
        }

        // GET api/values/5
     //   public string Get(int id)
       // {
          //  var userName = this.RequestContext.Principal.Identity.Name;

            //return String.Format("Hello, {0}.", userName);
        //}

        // POST api/values
        public string Post()
        {
            var userId = this.RequestContext.Principal.Identity.GetUserId();
            return userId;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
