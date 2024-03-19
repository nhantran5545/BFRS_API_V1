using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class BreedingReason
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BreedingReasonId { get; set; }
        public int? BreedingId { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string? Status { get; set; }

        public virtual Breeding? Breeding { get; set; }
        public virtual Account? CreatedByNavigation { get; set; }
    }
}
