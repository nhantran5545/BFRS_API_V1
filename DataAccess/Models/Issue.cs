using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class Issue
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IssueId { get; set; }
        public string? IssueName { get; set; }
        public Guid? BreedingId { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? AssignedTo { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? IssueTypeId { get; set; }
        public string? Status { get; set; }

        public virtual Account? AssignedToNavigation { get; set; }
        public virtual Breeding? Breeding { get; set; }
        public virtual Account? CreatedByNavigation { get; set; }
        public virtual IssueType? IssueType { get; set; }
        public virtual Account? UpdatedByNavigation { get; set; }
    }
}
