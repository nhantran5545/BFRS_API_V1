using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class ClutchDetailResponse
    {
        public Guid ClutchId { get; set; }
        public Guid? BreedingId { get; set; }
        public DateTime? BroodStartDate { get; set; }
        public DateTime? BroodEndDate { get; set; }
        public Guid? CageId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public string? CreatedByFirstName { get; set; }
        public string? CreatedByLastName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? UpdatedByFirstName { get; set; }
        public string? UpdatedByLastName { get; set; }
        public string? Status { get; set; }
        public List<EggResponse>? EggResponses { get; set; }
    }
}
