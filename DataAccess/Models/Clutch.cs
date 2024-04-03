using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Clutch
    {
        public Clutch()
        {
            BreedingCheckLists = new HashSet<BreedingCheckList>();
            ClutchReasons = new HashSet<ClutchReason>();
            Eggs = new HashSet<Egg>();
        }

        public int ClutchId { get; set; }
        public int BreedingId { get; set; }
        public DateTime? BroodStartDate { get; set; }
        public DateTime? BroodEndDate { get; set; }
        public int? CageId { get; set; }
        public int? Phase { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string? Status { get; set; }

        public virtual Breeding? Breeding { get; set; }
        public virtual Cage? Cage { get; set; }
        public virtual Account? CreatedByNavigation { get; set; }
        public virtual Account? UpdatedByNavigation { get; set; }
        public virtual ICollection<BreedingCheckList> BreedingCheckLists { get; set; }
        public virtual ICollection<ClutchReason> ClutchReasons { get; set; }
        public virtual ICollection<Egg> Eggs { get; set; }
    }
}
