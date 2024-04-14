using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Egg
    {
        public Egg()
        {
            BreedingCheckLists = new HashSet<BreedingCheckList>();
            EggBirds = new HashSet<EggBird>();
            EggStatusChanges = new HashSet<EggStatusChange>();
        }

        public int EggId { get; set; }
        public int ClutchId { get; set; }
        public DateTime? LayDate { get; set; }
        public DateTime? HatchedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string? Status { get; set; }

        public virtual Clutch? Clutch { get; set; }
        public virtual Account? CreatedByNavigation { get; set; }
        public virtual Account? UpdatedByNavigation { get; set; }
        public virtual ICollection<BreedingCheckList> BreedingCheckLists { get; set; }
        public virtual ICollection<EggBird> EggBirds { get; set; }
        public virtual ICollection<EggStatusChange> EggStatusChanges { get; set; }
    }
}
