using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.BirdReqModels
{
    public class BirdUpdateRequest
    {
        public int BirdId { get; set; }
        public string? BirdName { get; set; }
        public string? Gender { get; set; }
        public DateTime? HatchedDate { get; set; }
        public string? PurchaseFrom { get; set; }
        public DateTime? AcquisitionDate { get; set; }
        public int? BirdSpeciesId { get; set; }
        public int? CageId { get; set; }
        public int? FarmId { get; set; }
        public int? FatherBirdId { get; set; }
        public int? MotherBirdId { get; set; }
        public int? BandNumber { get; set; }
        public string? Image { get; set; }
        public string? LifeStage { get; set; }
        public string? Status { get; set; }
        public List<MutationRequest>? MutationRequests { get; set; }
    }
}
