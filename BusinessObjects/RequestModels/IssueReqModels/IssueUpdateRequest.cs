using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.IssueReqModels
{
    public class IssueUpdateRequest
    {
        public string? ProcessNote { get; set; }
        public string? Status { get; set; }
    }
}
