using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBooking.DAL.Models
{
   public class Message
    {


        public int MessageId { get; set; }
        [Required]
        [DisplayName("Content")]
        public string Content { get; set; }

        public string SenderId { get; set; }
        public string senderName { get; set; }

        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }

        public int AccomodationId { get; set; }
        public virtual Accomodation Accomodation { get; set; }

        public DateTime SentDate { get; set; }
        public bool Seen { get; set; }

    }
}
