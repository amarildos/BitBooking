using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBooking.DAL.Models
{
  public  class Token
    {

        public int TokenId { get; set; }
         [DisplayName("Email")]
         [Required]
        public string Hash { get; set; }

        public bool Used { get; set; }
        [DisplayName("Accomodation Name/Invitation for")]
        [Required]
        public int AccomodationId { get; set; }


    }
}
