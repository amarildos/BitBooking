using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BitBooking.API.Models;
using BitBooking.DAL.Models;
using PayPal.Api;
using System.Web;

namespace BitBooking.API.Controllers
{
    public class PaypalController : ApiController
    {
        private BitBooking.DAL.Models.ApplicationDbContext db = new DAL.Models.ApplicationDbContext();

        [Route("api/PayPal/PostCreatePayment")]
        public IHttpActionResult PostCreatePayment([FromBody]PayPalPaymentConfirmation model)
        {
            var userName = this.User.Identity.Name;

            string userId = "";

            try
            {
                userId = db.Users.SingleOrDefault(x => x.Email == userName).Id;
            }
            catch
            {
                return BadRequest("Unregistered user");
            }

            List<RoomAvailability> listOfUnpaidRooms = new List<RoomAvailability>();
            List<ReservationsPaymentAPI> listOfUnpaidRoomsFullDetails = new List<ReservationsPaymentAPI>();
            if (userId != null)
            {
                listOfUnpaidRooms = db.RoomAvailabilities.Where(x => x.UserId == userId && x.IsPaid == false).ToList();
            }
            if (listOfUnpaidRooms.Count > 0)
            {
                foreach (var room in listOfUnpaidRooms)
                {
                    string accomodationName = db.Accomodations.Single(x => x.AccomodationId == room.AccomodationId).AccomodationName;
                    string roomName = db.Rooms.Single(x => x.RoomId == room.RoomId).RoomType.RoomTypeName;

                    listOfUnpaidRoomsFullDetails.Add(new ReservationsPaymentAPI 
                    { 
                        AccommodationName =  accomodationName,
                        RoomName = roomName,
                        AccomodationId = room.AccomodationId,
                        ArrivalDate = room.ArrivalDate,
                        DepartureDate = room.DepartureDate,
                        IsPaid = room.IsPaid,
                        RoomAvailabilityId = room.RoomAvailabilityId,
                        RoomId = room.RoomId,
                        TotalPrice = room.TotalPrice,
                        UserId = room.UserId,
                        UserEmail = userName
                    });
                }
            }

            //BEGIN OF PAYPAL PAYMENT

            var config = ConfigManager.Instance.GetProperties();
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);
            apiContext.Config = config;
            Amount amount = new Amount();

            amount.total = listOfUnpaidRoomsFullDetails.Sum(x => x.TotalPrice).ToString();
            amount.currency = "USD";

            Transaction transaction = new Transaction();
            transaction.amount = amount;
            transaction.item_list = new ItemList();
            transaction.item_list.items = new List<Item>();

            List<Item> listOfRoomsToPay = new List<Item>();
            foreach(var room in listOfUnpaidRoomsFullDetails)
            {
                listOfRoomsToPay.Add(new Item
                {
                    name = room.RoomName,
                    description = room.AccommodationName,
                    quantity = "1",
                    price = room.TotalPrice.ToString(),
                    currency = "USD"
                });
            }


            transaction.item_list.items.AddRange(listOfRoomsToPay);

            Payer payer = new Payer();
            payer.payment_method = "paypal";

            Payment payment = new Payment();
            payment.intent = "sale";
            payment.payer = payer;
            payment.transactions = new List<Transaction>();
            payment.transactions.Add(transaction);
            payment.redirect_urls = new RedirectUrls();
            payment.redirect_urls.return_url = String.Format("http://{0}{1}", HttpContext.Current.Request.Url.Authority, "/api/PayPal?userId=" + userId);
            payment.redirect_urls.cancel_url = String.Format("http://{0}{1}", HttpContext.Current.Request.Url.Authority, "/api/PayPal");
  
            Payment createdPayment = payment.Create(apiContext);

            return Ok(createdPayment.GetApprovalUrl());
        }

        
        public IHttpActionResult GetPaymentApproved(string paymentId, string token, string PayerId,string userId)
        {
            var config = ConfigManager.Instance.GetProperties();
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);

            apiContext.Config = config;

            Payment payment = Payment.Get(apiContext, paymentId);

            PaymentExecution paymentExecution = new PaymentExecution();
            paymentExecution.payer_id = PayerId;

            try
            {
                Payment executedPayment = payment.Execute(apiContext, paymentExecution); //this PAYLEMT (EXECUTE) CAN FAIL
                if(executedPayment.state != "approved")
                {
                    return BadRequest("PayPal payment has failed. Please try again!");
                }
            }
            catch
            {
                return BadRequest("PayPal payment failed!");
            }

            
            //Set unpaid rooms to paid

            List<RoomAvailability> listOfUnpaidRooms = new List<RoomAvailability>();
            List<ReservationsPaymentAPI> listOfUnpaidRoomsFullDetails = new List<ReservationsPaymentAPI>();
            if (userId != null)
            {
                listOfUnpaidRooms = db.RoomAvailabilities.Where(x => x.UserId == userId && x.IsPaid == false).ToList();
            }
            if (listOfUnpaidRooms.Count > 0)
            {
                foreach (var room in listOfUnpaidRooms)
                {
                    room.IsPaid = true;
                    room.PayPalPaymentId = paymentId;
                    db.Entry(room).State = System.Data.Entity.EntityState.Modified;
                }
            }
            db.SaveChanges();

            string returnUrl = String.Format("http://{0}{1}", HttpContext.Current.Request.Url.Authority, "/#/paymentConfirmation");

            return Redirect(returnUrl);
        }

        public IHttpActionResult GetPaymentCanceled()
        {

            string returnUrl = String.Format("http://{0}{1}", HttpContext.Current.Request.Url.Authority, "/#/paymentCanceled");

            return Redirect(returnUrl);
        }
    }
}