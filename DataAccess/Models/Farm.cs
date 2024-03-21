using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Farm
    {
        public Farm()
        {
            Areas = new HashSet<Area>();
            Birds = new HashSet<Bird>();
        }

        public int FarmId { get; set; }
        public string? FarmName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<Bird> Birds { get; set; }
    }
}
