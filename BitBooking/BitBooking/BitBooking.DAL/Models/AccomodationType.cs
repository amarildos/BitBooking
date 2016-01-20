﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBooking.DAL.Models
{
    public class AccomodationType
    {
        public int AccomodationTypeId { get; set; }
        [Required]
        [DisplayName("Accomodation Type")]
        public string AccomodationTypeName { get; set; }
    }
}
