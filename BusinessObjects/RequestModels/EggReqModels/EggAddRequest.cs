using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.EggReqModels
{
    public class EggAddRequest
    {
        public int ClutchId { get; set; }
        public DateTime LayDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
