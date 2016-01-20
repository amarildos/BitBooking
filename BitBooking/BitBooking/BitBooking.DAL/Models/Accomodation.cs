using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace BitBooking.DAL.Models
{
    public class Accomodation 
    {
        public int AccomodationId { get; set; }
        [Required]
        [DisplayName("Accomodation Name")]
        public string AccomodationName { get; set; }

        [Range(1,5)]
        [DisplayName("Star Rating")]
        public int StarRating { get; set; }
        [Required]
        [DisplayName("Number of Rooms")]
        [Range(1,int.MaxValue)]
        public int NumberOfRooms { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }
        
        public virtual AccomodationType AccomodationType { get; set; }
        [DisplayName("Accomodation Type")]
        [Required]
        public int AccomodationTypeId { get; set; }

        [DisplayName("List of Rooms")]
        public virtual List<Room> ListOfRooms { get; set; }

        //Accomodation services list of services
        [DisplayName("Accomodation Services")]
        public virtual List<AccomodationService> ListOfAccomodationServices { get; set; }

        [DisplayName("Accomodation Facilities")]
        public virtual List<AccomodationFacility> ListOfAccomodationFacilities { get; set; }

        [DisplayName("Accomodation Information")]
        public int AccomodationInfoId { get; set; }
        public virtual AccomodationInfo AccomodationInfo { get; set; }

        [DisplayName("Gallery")]
        public virtual List<Photo> ListOfPhotos { get; set; }

        [DisplayName("Comments")]
        public virtual List<UserComment> ListUserComments { get; set; }

     
    }
}
