using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Http;

namespace BitBooking.API.Models
{
    // Models returned by AccountController actions.

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class PayPalPaymentConfirmation
    {
        public string paymentId { get; set; }
        public string token { get; set; }
        public string PayerId { get; set; }
        public string userId { get; set; }
    }

    public class ReservationsPaymentAPI
    {
        public int RoomAvailabilityId { get; set; }

        public int RoomId { get; set; }

        public int AccomodationId { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ArrivalDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DepartureDate { get; set; }

        [Required]
        public string UserId { get; set; }

        public bool IsPaid { get; set; }
        
        [Range(1, int.MaxValue)]
        public double TotalPrice { get; set; }

        public string AccommodationName { get; set; }
        public string RoomName { get; set; }

        public string UserEmail { get; set; }

    }

    public class UserCommentReportAPI
    {
        public int CommentId { get; set; }
        public string ApplicationUserId { get; set; }
        public int AccomodationIdValue { get; set; }
    }

    public class UserCommentsAPI
    {
        public int UserCommentId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Comment { get; set; }

        public string ApplicationUserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Range(1, 5)]
        public double Rating { get; set; }

        public int ReportCount { get; set; }

        public int AccomodationId { get; set; }

    }

    public class ReserveConfirmationModel
    {
        public int? id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public bool IsPaid { get; set; }
        public double TotalPrice { get; set; }
        public string UserId { get; set; }
        public double Price { get; set; }
    }

    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class SearchModel
    {
        public string content { get; set; }

        public string SearchCity { get; set; }
        public string SearchCountry { get; set; }

        public DateTime Arrival_date { get; set; }

        public DateTime Departure_date { get; set; }

        public int number_persons { get; set; }
        
        public int peopleCapacity { get; set; }

        public int NumberOfStarsSearch { get; set; }

        public double TotalPriceSearch { get; set; }
    }

    public class RoomSearchModel
    {
        public int NumberOfRooms { get; set; }
        public double Price { get; set; }
        public int AccomodationId { get; set; }
        public string AccomodationName { get; set; }
        public int AccommodationStars { get; set; }
        public int RoomCapacity { get; set; }
        public string RoomDetails { get; set; }
        public int RoomId { get; set; }
        public string RoomType_Name { get; set; }
        public DateTime TempArrivalDate { get; set; }
        public DateTime TempDepartureDate { get; set; }
        public int RoomTypeId { get; set; }
        public bool TempIsPaid { get; set; }
        public double TempTotalPrice { get; set; }
        public string TempUserId { get; set; }
        public string TempReservationString { get; set; }
        public string PhotoUrl { get; set; }
    }


    
    public class UserInfoViewModel
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
