using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBooking.DAL.Models
{
    public class CommentUserReport
    {
        public int CommentUserReportId { get; set; }

        public string ApplicationUserId { get; set; }

        public int AccomodationId { get; set; }

        public int CommentId { get; set; }

        public bool IsReported { get; set; }
    }
}
