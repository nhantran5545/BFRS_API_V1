using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class IssueType
    {
        public IssueType()
        {
            Issues = new HashSet<Issue>();
        }

        public Guid IssueTypeId { get; set; }
        public string? IssueName { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }
    }
}
