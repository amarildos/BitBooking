using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBooking.DAL.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        
        //Number of rooms of this type
        [DisplayName("Number of rooms")]
        [Range(1,int.MaxValue)]
        public int NumberOfRooms { get; set; }

        [DisplayName("Price per night")]
        [Required]
        [Range(1, int.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public double Price { get; set; }

        [DisplayName("Room Capacity")]
        [Required]
        [Range(1, int.MaxValue)]
        public int RoomCapacity { get; set; }
        
        //Room reference
        public virtual RoomType RoomType { get; set; }
        [DisplayName("Room Type")]
        [Required]
        public int RoomTypeId { get; set; }
        
        //Accomodation reference
        public virtual Accomodation Accomodation { get; set; }
        [DisplayName("Accomodation Name")]
        public int AccomodationId { get; set; }
        //List of available rooms
        [DisplayName("List of Available Rooms")]
        public virtual List<RoomAvailability> ListOfAvailableRooms { get; set; }

        //Temporary properties used to reserve a ROOM

        [NotMapped]
        public string TempReservationString { get; set; }

        [NotMapped]
        [DisplayName("Arrival date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TempArrivalDate { get; set; }

        [NotMapped]
        [DisplayName("Departure date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TempDepartureDate { get; set; }

        [NotMapped]
        [DisplayName("User ID")]
        public string TempUserId { get; set; }

        [NotMapped]
        [DisplayName("Is Paid")]
        public bool TempIsPaid { get; set; }

        [NotMapped]
        [DisplayName("Total price")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public double TempTotalPrice { get; set; }

        [Required]
        [DisplayName("Details")]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string RoomDetails { get; set; }
    }
}
