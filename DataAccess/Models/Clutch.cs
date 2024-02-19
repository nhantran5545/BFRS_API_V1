using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class Clutch
    {
        public Clutch()
        {
            ClutchReasons = new HashSet<ClutchReason>();
            Eggs = new HashSet<Egg>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ClutchId { get; set; }
        public Guid? BreedingId { get; set; }
        public DateTime? BroodStartDate { get; set; }
        public DateTime? BroodEndDate { get; set; }
        public Guid? CageId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? Status { get; set; }

        public virtual Breeding? Breeding { get; set; }
        public virtual Cage? Cage { get; set; }
        public virtual Account? CreatedByNavigation { get; set; }
        public virtual Account? UpdatedByNavigation { get; set; }
        public virtual ICollection<ClutchReason> ClutchReasons { get; set; }
        public virtual ICollection<Egg> Eggs { get; set; }
    }
}
