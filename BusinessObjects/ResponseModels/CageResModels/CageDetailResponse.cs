using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels.CageResModels
{
    public class CageDetailResponse
    {
        public int CageId { get; set; }
        public DateTime? ManufacturedDate { get; set; }
        public string? ManufacturedAt { get; set; }
        public DateTime? PurchasedDate { get; set; }
        public int AreaId { get; set; }
        public string? AreaName { get; set; }
        public int AccountId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int BirdQuantity { get; set; }
        public string? Status { get; set; }
    }
}
