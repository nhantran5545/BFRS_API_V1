using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels
{
    public class EggUpdateRequest
    {
        public int EggId { get; set; }
        //public int UpdatedBy { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
