using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBooking.DAL.Models
{
    public class AccomodationService
    {
        
        public int AccomodationServiceId { get; set; }
        [DisplayName("Service Name")]
        [Required]
        public string Name { get; set; }
        [DisplayName("Accomodation ID")]
        public int AccomodationId { get; set; }
        
        public virtual AccomodationServiceType AccomodationServiceType { get; set; }
        [DisplayName("Service Category")]
        [Required]
        public int AccomodationServiceTypeId { get; set; }
    }
}
