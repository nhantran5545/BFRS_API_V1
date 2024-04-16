using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.CageReqModels
{
    public class CageUpdateRequest
    {
        public DateTime? ManufacturedDate { get; set; }
        public string? ManufacturedAt { get; set; }
        public DateTime? PurchasedDate { get; set; }
        public int AreaId { get; set; }
        public int AccountId { get; set; }
    }
}
