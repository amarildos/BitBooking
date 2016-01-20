using BitBooking.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace BitBooking.API.Controllers
{
    public class AccomodationSuggestionController : ApiController
    {
        public AccomodationSuggestionController()
        {

            db.Configuration.ProxyCreationEnabled = false;
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AccomodationsTest
        public IQueryable<Object> GetAccomodations()
        {
            return db.Accomodations.Include(a => a.AccomodationInfo)
                .Select(x => new { x.AccomodationId, x.AccomodationInfo, x.AccomodationName }).Distinct();
        }
    }
}
