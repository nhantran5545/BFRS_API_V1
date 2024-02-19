using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class BreedingCheckListDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BreedingCheckListDetailId { get; set; }
        public Guid? CheckListId { get; set; }
        public Guid? BreedingId { get; set; }
        public Guid? CheckListDetailId { get; set; }
        public DateTime? CheckDate { get; set; }
        public int? CheckValue { get; set; }
        public bool? Compulsory { get; set; }
        public bool? Positive { get; set; }
        public string? Status { get; set; }

        public virtual Breeding? Breeding { get; set; }
        public virtual CheckList? CheckList { get; set; }
        public virtual CheckListDetail? CheckListDetail { get; set; }
    }
}
