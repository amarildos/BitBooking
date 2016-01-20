using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBooking.DAL.Models
{
    public class AccomodationInfo
    {
        public int AccomodationInfoId { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        public string Country { get; set; }
        [DisplayName("Telephone Number")]
        public string Phone { get; set; }
        [DisplayName("Email Address")]
        [Required]
        public string Email { get; set; }

        [DisplayName("Accomodation Name")]
        public int AccomodationId { get; set; }

        public string GoogleX { get; set; }
        public string GoogleY { get; set; }
    }
}
