using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels.BirdSpeciesResModels
{
    public class BirdSpeciesDetailResponse
    {
        public int BirdSpeciesId { get; set; }
        public string? BirdSpeciesName { get; set; }
        public string? Description { get; set; }
        public int BirdTypeId { get; set; }
        public string? BirdTypeName { get; set; }
        public int? HatchingPhaseFrom { get; set; }
        public int? HatchingPhaseTo { get; set; }
        public int? NestlingPhaseFrom { get; set; }
        public int? NestlingPhaseTo { get; set; }
        public int? ChickPhaseFrom { get; set; }
        public int? ChickPhaseTo { get; set; }
        public int? FledgingPhaseFrom { get; set; }
        public int? FledgingPhaseTo { get; set; }
        public int? JuvenilePhaseFrom { get; set; }
        public int? JuvenilePhaseTo { get; set; }
        public int? ImmaturePhaseFrom { get; set; }
        public int? ImmaturePhaseTo { get; set; }
        public int? AdultPhaseFrom { get; set; }
        public int? AdultPhaseTo { get; set; }
        public List<IndividualMutation>? IndividualMutations { get; set; }
    }
}
