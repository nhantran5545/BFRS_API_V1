using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels
{
    public class EggHatchRequest
    {
        public int EggId { get; set; }
        public DateTime? HatchedDate { get; set; }
        //public int UpdatedBy { get; set; }
    }
}
