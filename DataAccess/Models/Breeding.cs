using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class Breeding
    {
        public Breeding()
        {
            BreedingCheckListDetails = new HashSet<BreedingCheckListDetail>();
            BreedingReasons = new HashSet<BreedingReason>();
            Clutches = new HashSet<Clutch>();
            Issues = new HashSet<Issue>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BreedingId { get; set; }
        public Guid? FatherBirdId { get; set; }
        public Guid? MotherBirdId { get; set; }
        public bool? CoupleSeperated { get; set; }
        public Guid? CageId { get; set; }
        public DateTime? NextCheck { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? Status { get; set; }

        public virtual Cage? Cage { get; set; }
        public virtual Account? CreatedByNavigation { get; set; }
        public virtual Bird? FatherBird { get; set; }
        public virtual Bird? MotherBird { get; set; }
        public virtual Account? UpdatedByNavigation { get; set; }
        public virtual ICollection<BreedingCheckListDetail> BreedingCheckListDetails { get; set; }
        public virtual ICollection<BreedingReason> BreedingReasons { get; set; }
        public virtual ICollection<Clutch> Clutches { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
    }
}
