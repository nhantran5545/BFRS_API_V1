using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class BreedingDetailResponse
    {
        public Guid BreedingId { get; set; }
        public Guid? FatherBirdId { get; set; }
        public Guid? MotherBirdId { get; set; }
        public bool? CoupleSeperated { get; set; }
        public Guid? CageId { get; set; }
        public DateTime? NextCheck { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? Status { get; set; }
    }
}
