using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class CheckListDetail
    {
        public CheckListDetail()
        {
            BreedingCheckListDetails = new HashSet<BreedingCheckListDetail>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CheckListDetailId { get; set; }
        public Guid? CheckListId { get; set; }
        public string? QuestionName { get; set; }
        public string? Frequency { get; set; }
        public string? Status { get; set; }

        public virtual CheckList? CheckList { get; set; }
        public virtual ICollection<BreedingCheckListDetail> BreedingCheckListDetails { get; set; }
    }
}
