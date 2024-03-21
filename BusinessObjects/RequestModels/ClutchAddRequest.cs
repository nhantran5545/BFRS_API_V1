﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels
{
    public class ClutchAddRequest
    {
        public int? BreedingId { get; set; }
        public DateTime? BroodStartDate { get; set; }
        public int? CageId { get; set; }
        public int? CreatedBy { get; set; }
    }
}