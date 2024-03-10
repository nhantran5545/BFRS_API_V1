using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Egg
    {
        public Egg()
        {
            EggBirds = new HashSet<EggBird>();
            EggReasons = new HashSet<EggReason>();
        }

        public Guid EggId { get; set; }
        public Guid? ClutchId { get; set; }
        public DateTime? LayDate { get; set; }
        public DateTime? HatchedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? Status { get; set; }

        public virtual Clutch? Clutch { get; set; }
        public virtual Account? CreatedByNavigation { get; set; }
        public virtual Account? UpdatedByNavigation { get; set; }
        public virtual ICollection<EggBird> EggBirds { get; set; }
        public virtual ICollection<EggReason> EggReasons { get; set; }
    }
}
