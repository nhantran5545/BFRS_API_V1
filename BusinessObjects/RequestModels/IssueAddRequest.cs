using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels
{
    public class IssueAddRequest
    {
        public string? IssueName { get; set; }
        public int? BreedingId { get; set; }
        public string? Description { get; set; }
        public int? IssueTypeId { get; set; }
    }
}
