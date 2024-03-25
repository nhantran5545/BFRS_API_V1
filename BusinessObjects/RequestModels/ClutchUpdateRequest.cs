using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels
{
    public class ClutchUpdateRequest
    {
        public int ClutchId { get; set; }
        public int UpdatedBy { get; set; }
        public string? Status { get; set; }
    }
}
