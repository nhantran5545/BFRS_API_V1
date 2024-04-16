using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels.IssueResModels
{
    public class IssueResponse
    {
        public int IssueId { get; set; }
        public string? IssueName { get; set; }
        public int? BreedingId { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string? CreatedByFirstName { get; set; }
        public string? CreatedByLastName { get; set; }
        public int? AssignedTo { get; set; }
        public string? ProcessNote { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string? UpdatedByFirstName { get; set; }
        public string? UpdatedByLastName { get; set; }
        public int? IssueTypeId { get; set; }
        public string IssueTypeName { get; set; }
        public string? Status { get; set; }
    }
}
