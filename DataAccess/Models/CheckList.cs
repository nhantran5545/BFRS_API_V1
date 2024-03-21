using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class CheckList
    {
        public CheckList()
        {
            BreedingCheckLists = new HashSet<BreedingCheckList>();
            CheckListDetails = new HashSet<CheckListDetail>();
        }

        public int CheckListId { get; set; }
        public int? Phase { get; set; }
        public string? CheckListName { get; set; }
        public int? SpeciesId { get; set; }
        public string? Status { get; set; }

        public virtual BirdSpecy? Species { get; set; }
        public virtual ICollection<BreedingCheckList> BreedingCheckLists { get; set; }
        public virtual ICollection<CheckListDetail> CheckListDetails { get; set; }
    }
}
