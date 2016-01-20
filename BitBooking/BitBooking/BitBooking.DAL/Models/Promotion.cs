using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBooking.DAL.Models
{
  public  class Promotion
    {



        public int PromotionId { get; set; }


        public int FirstAccomodationId { get; set; }
        public int SecondAccomodationId { get; set; }
        public int ThirdAccomodationId { get; set; }

        public int PromotionType { get; set; }
      

    }
}
