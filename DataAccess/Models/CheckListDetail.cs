using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class CheckListDetail
    {
        public CheckListDetail()
        {
            BreedingCheckListDetails = new HashSet<BreedingCheckListDetail>();
        }

        public int CheckListDetailId { get; set; }
        public int? CheckListId { get; set; }
        public string? QuestionName { get; set; }
        public bool? Compulsory { get; set; }
        public bool? Positive { get; set; }
        public int? Priority { get; set; }
        public string? Status { get; set; }

        public virtual CheckList? CheckList { get; set; }
        public virtual ICollection<BreedingCheckListDetail> BreedingCheckListDetails { get; set; }
    }
}
