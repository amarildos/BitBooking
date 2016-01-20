using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBooking.DAL.Models
{
    public class Photo
    {
        public int PhotoId { get; set; }
        [Required]
        [DisplayName("Photo")]
        public string PhotoUrl { get; set; }

        public int AccomodationId { get; set; }
     
        public virtual Accomodation Accomodation { get; set; }
         [Required]
         [DisplayName("Room Type")]
        public int RoomTypeId { get; set; }
        public virtual RoomType RoomType { get; set; }
        public int Priority { get; set; }
        public int FacilityId { get; set; }
    }
}
