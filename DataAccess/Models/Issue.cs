using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Issue
    {
        public int IssueId { get; set; }
        public string? IssueName { get; set; }
        public int? BreedingId { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? AssignedTo { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? IssueTypeId { get; set; }
        public string? Status { get; set; }

        public virtual Account? AssignedToNavigation { get; set; }
        public virtual Breeding? Breeding { get; set; }
        public virtual Account? CreatedByNavigation { get; set; }
        public virtual IssueType? IssueType { get; set; }
        public virtual Account? UpdatedByNavigation { get; set; }
    }
}
