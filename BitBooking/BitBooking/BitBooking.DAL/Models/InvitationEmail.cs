using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BitBooking.DAL.Models
{
   public class InvitationEmail:Email
    {
      
            public string To { get; set; }
            public string UserName { get; set; }
            public string Link { get; set; }
        
    }
}
