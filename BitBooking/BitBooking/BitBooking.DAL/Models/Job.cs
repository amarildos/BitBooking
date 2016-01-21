using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBooking.DAL.Models
{
   public class Job
    {

        public int JobId { get; set; }
        [DisplayName("Position Name")]
        [Required]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("Description")]
        public string Description { get; set; }

           [Required]
        [DisplayName("Salary")]
        public int Salary { get; set; }
        public int AccomodationId { get; set; }
        public virtual Accomodation Accomodation { get; set; }

    }
}
