using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBooking.DAL.Models
{
    public class AccomodationFacility
    {
        public int AccomodationFacilityId { get; set; }
        [DisplayName("Facility Name")]
        [Required]
        public string Name { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Start Hours")]
        [Required]
        [DataType(DataType.Time)]
        public DateTime StartHours { get; set; }
        
        [DisplayName("End Hours")]
        [Required]
        [DataType(DataType.Time)]
        public DateTime EndHours { get; set; }
        [DisplayName("Accomodation Name")]
        public int AccomodationId { get; set; }
        public virtual Accomodation Accomodation { get; set; }

        public string PhotoUrl { get; set; }
    }
}
