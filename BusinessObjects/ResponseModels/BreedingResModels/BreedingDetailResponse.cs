using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.ResponseModels.ClutchResModels;

namespace BusinessObjects.ResponseModels.BreedingResModels
{
    public class BreedingDetailResponse
    {
        public int BreedingId { get; set; }
        public int FatherBirdId { get; set; }
        public int FatherBandNumber { get; set; }
        public string? FatherImage { get; set; }
        public int MotherBirdId { get; set; }
        public int MotherBandNumber { get; set; }
        public string? MotherImage { get; set; }
        public int SpeciesId { get; set; }
        public string? SpeciesName { get; set; }
        public bool? CoupleSeperated { get; set; }
        public int CageId { get; set; }
        public int Phase { get; set; }
        public DateTime? NextCheck { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string? Status { get; set; }
        public List<ClutchResponse>? ClutchResponses { get; set; }
    }
}
