using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBooking.DAL.Models
{
    public class RoomAvailability
    {
        public int RoomAvailabilityId { get; set; }

        [DisplayName("Room")]
        public int RoomId { get; set; }
        //public virtual Room Room { get; set; }

        [DisplayName("Accomodation Name")]
        public int AccomodationId { get; set; }
        //public virtual Accomodation Accomodation { get; set; }
        [Required]
        [DisplayName("Arrival date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ArrivalDate { get; set; }

        [Required]
        [DisplayName("Departure date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DepartureDate { get; set; }

        [DisplayName("Reserved by:")]
        [Required]
        public string UserId { get; set; }

         [DisplayName("Is Paid")]
        public bool IsPaid { get; set; }
        [Required]
        [DisplayName("Total price")]
        [Range(1,int.MaxValue)]
        public double TotalPrice { get; set; }

        public string PayPalPaymentId { get; set; }
        public string UserEmail { get; set; }
    }
}
