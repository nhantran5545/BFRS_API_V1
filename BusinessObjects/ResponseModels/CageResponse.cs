using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class CageResponse
    {
        public Guid CageId { get; set; }
        public DateTime? ManufacturedDate { get; set; }
        public Guid? AreaId { get; set; }
        public string? AreaName { get; set; }
        public Guid? AccountId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Status { get; set; }
    }
}
