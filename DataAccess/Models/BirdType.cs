using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class BirdType
    {
        public BirdType()
        {
            BirdSpecies = new HashSet<BirdSpecy>();
        }

        public int BirdTypeId { get; set; }
        public string? BirdTypeName { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<BirdSpecy> BirdSpecies { get; set; }
    }
}
