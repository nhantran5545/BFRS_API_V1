﻿using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class BreedingReason
    {
        public Guid BreedingReasonId { get; set; }
        public Guid? BreedingId { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public string? Status { get; set; }

        public virtual Breeding? Breeding { get; set; }
        public virtual Account? CreatedByNavigation { get; set; }
    }
}