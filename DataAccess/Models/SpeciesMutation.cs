using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class SpeciesMutation
    {
        public int BirdSpeciesId { get; set; }
        public int MutationId { get; set; }
        public string? Status { get; set; }

        public virtual BirdSpecy BirdSpecies { get; set; } = null!;
        public virtual Mutation Mutation { get; set; } = null!;
    }
}
