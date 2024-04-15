using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class IssueTypeResponse
    {
        public int IssueTypeId { get; set; }
        public string? IssueName { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}
