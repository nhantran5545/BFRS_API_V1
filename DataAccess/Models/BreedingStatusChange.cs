using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class BreedingStatusChange
    {
        public int StatusChangeId { get; set; }
        public int? BreedingId { get; set; }
        public string? Description { get; set; }
        public DateTime? ChangedDate { get; set; }
        public int? ChangedBy { get; set; }
        public string? NewStatus { get; set; }
        public string? OldStatus { get; set; }

        public virtual Breeding? Breeding { get; set; }
        public virtual Account? ChangedByNavigation { get; set; }
    }
}
