using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Mutation
    {
        public Mutation()
        {
            BirdMutations = new HashSet<BirdMutation>();
            SpeciesMutations = new HashSet<SpeciesMutation>();
        }

        public Guid MutationId { get; set; }
        public string? MutationName { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<BirdMutation> BirdMutations { get; set; }
        public virtual ICollection<SpeciesMutation> SpeciesMutations { get; set; }
    }
}
