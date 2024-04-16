using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels.BirdResModels
{
    public class BirdDetailResponse
    {
        public int BirdId { get; set; }
        public string? BirdName { get; set; }
        public int? BandNumber { get; set; }
        public string? Gender { get; set; }
        public DateTime? HatchedDate { get; set; }
        public string? PurchaseFrom { get; set; }
        public DateTime? AcquisitionDate { get; set; }
        public int CageId { get; set; }
        public int? FarmId { get; set; }
        public int BirdSpeciesId { get; set; }
        public string? BirdSpeciesName { get; set; }
        public string? Image { get; set; }
        public List<IndividualMutation>? IndividualMutations { get; set; }
        public string? LifeStage { get; set; }
        public string? Status { get; set; }
    }
}
