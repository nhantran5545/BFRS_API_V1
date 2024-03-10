using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class BreedingCheckListDetail
    {
        public Guid BreedingCheckListDetailId { get; set; }
        public Guid? BreedingId { get; set; }
        public Guid? CheckListDetailId { get; set; }
        public DateTime? CheckDate { get; set; }
        public int? CheckValue { get; set; }
        public bool? Compulsory { get; set; }
        public bool? Positive { get; set; }
        public string? Status { get; set; }

        public virtual Breeding? Breeding { get; set; }
        public virtual CheckListDetail? CheckListDetail { get; set; }
    }
}
