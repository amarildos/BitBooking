using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBooking.DAL.Models
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }
        [Required]
        [MaxLength(150)]
        [DisplayName("Room Type")]
        public string RoomTypeName { get; set; } //Single room,etc...

        [DisplayName("Gallery")]
        public virtual List<Photo> ListOfPhotos { get; set; }
    }
}
