using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class Area
    {
        public Area()
        {
            Cages = new HashSet<Cage>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AreaId { get; set; }
        public string? AreaName { get; set; }
        public string? Description { get; set; }
        public Guid? FarmId { get; set; }
        public string? Status { get; set; }

        public virtual Farm? Farm { get; set; }
        public virtual ICollection<Cage> Cages { get; set; }
    }
}
