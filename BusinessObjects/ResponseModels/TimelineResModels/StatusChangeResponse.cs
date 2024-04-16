using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels.TimelineResModels
{
    public class StatusChangeResponse
    {
        public int StatusChangeId { get; set; }
        public string? Description { get; set; }
        public DateTime? ChangedDate { get; set; }
        public int? ChangedBy { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? OldStatus { get; set; }
        public string? NewStatus { get; set; }
    }
}
