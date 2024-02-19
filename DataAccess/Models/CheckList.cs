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
            BreedingCheckListDetails = new HashSet<BreedingCheckListDetail>();
            CheckListDetails = new HashSet<CheckListDetail>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CheckListId { get; set; }
        public string? DurationName { get; set; }
        public string? CheckListName { get; set; }
        public Guid? SpeciesId { get; set; }
        public string? Status { get; set; }

        public virtual BirdSpecy? Species { get; set; }
        public virtual ICollection<BreedingCheckListDetail> BreedingCheckListDetails { get; set; }
        public virtual ICollection<CheckListDetail> CheckListDetails { get; set; }
    }
}
