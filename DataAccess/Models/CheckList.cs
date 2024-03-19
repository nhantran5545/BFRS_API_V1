using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class CheckList
    {
        public CheckList()
        {
            CheckListDetails = new HashSet<CheckListDetail>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CheckListId { get; set; }
        public string? DurationName { get; set; }
        public string? CheckListName { get; set; }
        public int? SpeciesId { get; set; }
        public string? Status { get; set; }

        public virtual BirdSpecy? Species { get; set; }
        public virtual ICollection<CheckListDetail> CheckListDetails { get; set; }
    }
}
