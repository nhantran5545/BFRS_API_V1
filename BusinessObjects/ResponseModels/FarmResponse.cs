using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class FarmResponse
    {
        public int FarmId { get; set; }
        public string? FarmName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}
