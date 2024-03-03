using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class BirdDetailResponse
    {
        public Guid BirdId { get; set; }
        public string? Sex { get; set; }
        public DateTime? HatchedDate { get; set; }
        public string? PurchaseFrom { get; set; }
        public DateTime? AcquisitionDate { get; set; }
        public Guid? CageId { get; set; }
        public Guid? BirdSpeciesId { get; set; }
        public string? BirdSpeciesName { get; set; }
        public string? Image { get; set; }
        public Guid? MutationId { get; set; }
        public string? MutationName { get; set; }
        public string? LifeStage { get; set; }
        public string? Status { get; set; }
    }
}
