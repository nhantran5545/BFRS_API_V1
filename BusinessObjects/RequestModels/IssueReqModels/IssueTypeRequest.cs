using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.IssueReqModels
{
    public class IssueTypeRequest
    {
        public string? IssueName { get; set; }
        public string? Description { get; set; }
    }
}
