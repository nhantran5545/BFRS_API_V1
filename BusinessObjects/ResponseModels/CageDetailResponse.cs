using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class CageDetailResponse
    {
        public Guid CageId { get; set; }
        public DateTime? ManufacturedDate { get; set; }
        public string? ManufacturedAt { get; set; }
        public DateTime? PurchasedDate { get; set; }
        public Guid? AreaId { get; set; }
        public string? AreaName { get; set; }
        public Guid? AccountId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int BirdQuantity { get; set; }
        public string? Status { get; set; }
    }
}
