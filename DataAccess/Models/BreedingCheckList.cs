using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class BreedingCheckList
    {
        public BreedingCheckList()
        {
            BreedingCheckListDetails = new HashSet<BreedingCheckListDetail>();
        }

        public int BreedingCheckListId { get; set; }
        public int? BreedingId { get; set; }
        public int? ClutchId { get; set; }
        public int? EggId { get; set; }
        public int? CheckListId { get; set; }
        public int Phase { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Status { get; set; }

        public virtual Breeding? Breeding { get; set; }
        public virtual CheckList? CheckList { get; set; }
        public virtual Clutch? Clutch { get; set; }
        public virtual Egg? Egg { get; set; }
        public virtual ICollection<BreedingCheckListDetail> BreedingCheckListDetails { get; set; }
    }
}
