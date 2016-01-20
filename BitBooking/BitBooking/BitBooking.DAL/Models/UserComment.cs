using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBooking.DAL.Models
{
    public class UserComment
    {
        public int UserCommentId { get; set; }
        
        [Required]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        
        public string ApplicationUserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Range(1,5)]
        public double Rating { get; set; }

        public int ReportCount { get; set; }

        public int AccomodationId { get; set; }

        public virtual Accomodation Accomodation { get; set; }
    }
}
