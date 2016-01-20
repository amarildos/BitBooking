using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBooking.DAL.Models
{
    public class RoomPrice
    {
        public int RoomPriceId { get; set; }

        [Required]
        [DisplayName("Room Type")]
        public int RoomTypeId { get; set; }
        public virtual RoomType RoomType { get; set; }
        [DisplayName("Accomodation Name")]
        public int AccomodationId { get; set; }
        public virtual Accomodation Accomodation { get; set; }
        [Required]
        [DisplayName("Special price")]
        [Range(1,int.MaxValue)]
        public double SpecialPrice { get; set; }
        [Required]
        [DisplayName("Start date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DisplayName("End date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

    }
}
