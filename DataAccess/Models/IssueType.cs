using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class IssueType
    {
        public IssueType()
        {
            Issues = new HashSet<Issue>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IssueTypeId { get; set; }
        public string? IssueName { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }
    }
}
