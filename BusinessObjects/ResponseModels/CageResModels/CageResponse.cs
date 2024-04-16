using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels.CageResModels
{
    public class CageResponse
    {
        public int CageId { get; set; }
        public DateTime? ManufacturedDate { get; set; }
        public int AreaId { get; set; }
        public DateTime? PurchasedDate { get; set; }
        public string? AreaName { get; set; }
        public int AccountId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Status { get; set; }
        public int? NumOfBird { get; set; }
    }
}
