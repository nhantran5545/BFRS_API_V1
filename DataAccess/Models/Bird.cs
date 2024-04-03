using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Bird
    {
        public Bird()
        {
            BirdMutations = new HashSet<BirdMutation>();
            BreedingFatherBirds = new HashSet<Breeding>();
            BreedingMotherBirds = new HashSet<Breeding>();
            EggBirds = new HashSet<EggBird>();
            InverseFatherBird = new HashSet<Bird>();
            InverseMotherBird = new HashSet<Bird>();
        }

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

        public virtual BirdSpecy? BirdSpecies { get; set; }
        public virtual Cage? Cage { get; set; }
        public virtual Farm? Farm { get; set; }
        public virtual Bird? FatherBird { get; set; }
        public virtual Bird? MotherBird { get; set; }
        public virtual ICollection<BirdMutation> BirdMutations { get; set; }
        public virtual ICollection<Breeding> BreedingFatherBirds { get; set; }
        public virtual ICollection<Breeding> BreedingMotherBirds { get; set; }
        public virtual ICollection<EggBird> EggBirds { get; set; }
        public virtual ICollection<Bird> InverseFatherBird { get; set; }
        public virtual ICollection<Bird> InverseMotherBird { get; set; }
    }
}
