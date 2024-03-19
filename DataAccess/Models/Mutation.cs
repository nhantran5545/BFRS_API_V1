using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class Mutation
    {
        public Mutation()
        {
            BirdMutations = new HashSet<BirdMutation>();
            SpeciesMutations = new HashSet<SpeciesMutation>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MutationId { get; set; }
        public string? MutationName { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<BirdMutation> BirdMutations { get; set; }
        public virtual ICollection<SpeciesMutation> SpeciesMutations { get; set; }
    }
}
