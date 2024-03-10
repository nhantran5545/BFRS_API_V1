using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class CheckList
    {
        public CheckList()
        {
            CheckListDetails = new HashSet<CheckListDetail>();
        }

        public Guid CheckListId { get; set; }
        public string? DurationName { get; set; }
        public string? CheckListName { get; set; }
        public Guid? SpeciesId { get; set; }
        public string? Status { get; set; }

        public virtual BirdSpecy? Species { get; set; }
        public virtual ICollection<CheckListDetail> CheckListDetails { get; set; }
    }
}
