using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBooking.DAL.Models
{
    public class AccomodationServiceType
    {
        public int AccomodationServiceTypeId { get; set; }
        [DisplayName("Category Name")]
        [Required]
        public string Name { get; set; }
    }
}
