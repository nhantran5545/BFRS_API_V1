using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class BreedingCheckListDetail
    {
        public int BreedingCheckListDetailId { get; set; }
        public int? CheckListDetailId { get; set; }
        public int? BreedingCheckListId { get; set; }
        public int? CheckValue { get; set; }
        public string? Status { get; set; }

        public virtual BreedingCheckList? BreedingCheckList { get; set; }
        public virtual CheckListDetail? CheckListDetail { get; set; }
    }
}
