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
        public DateTime? BroodStartDate { get; set; }
        public DateTime? BroodEndDate { get; set; }
        //public int? CageId { get; set; }
        //public int? UpdatedBy { get; set; }
    }
}
