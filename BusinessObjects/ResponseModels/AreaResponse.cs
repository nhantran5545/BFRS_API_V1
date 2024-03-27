using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class AreaResponse
    {
        public int AreaId { get; set; }
        public string? AreaName { get; set; }
        public string? Description { get; set; }
        public int? FarmId { get; set; }
        public string? FarmName { get; set; }
        public string? Status { get; set; }
    }
}
