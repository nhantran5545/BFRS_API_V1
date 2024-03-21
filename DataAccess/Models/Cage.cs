using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Cage
    {
        public Cage()
        {
            Birds = new HashSet<Bird>();
            Breedings = new HashSet<Breeding>();
            Clutches = new HashSet<Clutch>();
        }

        public int CageId { get; set; }
        public DateTime? ManufacturedDate { get; set; }
        public string? ManufacturedAt { get; set; }
        public DateTime? PurchasedDate { get; set; }
        public int? AreaId { get; set; }
        public int? AccountId { get; set; }
        public string? Status { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Area? Area { get; set; }
        public virtual ICollection<Bird> Birds { get; set; }
        public virtual ICollection<Breeding> Breedings { get; set; }
        public virtual ICollection<Clutch> Clutches { get; set; }
    }
}
