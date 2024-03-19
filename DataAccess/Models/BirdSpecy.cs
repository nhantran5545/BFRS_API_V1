using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class BirdSpecy
    {
        public BirdSpecy()
        {
            Birds = new HashSet<Bird>();
            BreedingNorms = new HashSet<BreedingNorm>();
            CheckLists = new HashSet<CheckList>();
            SpeciesMutations = new HashSet<SpeciesMutation>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BirdSpeciesId { get; set; }
        public string? BirdSpeciesName { get; set; }
        public string? Description { get; set; }
        public int? BirdTypeId { get; set; }
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
        public string? Status { get; set; }

        public virtual BirdType? BirdType { get; set; }
        public virtual ICollection<Bird> Birds { get; set; }
        public virtual ICollection<BreedingNorm> BreedingNorms { get; set; }
        public virtual ICollection<CheckList> CheckLists { get; set; }
        public virtual ICollection<SpeciesMutation> SpeciesMutations { get; set; }
    }
}
