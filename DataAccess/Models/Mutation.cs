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
            Birds = new HashSet<Bird>();
            SpeciesMutations = new HashSet<SpeciesMutation>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MutationId { get; set; }
        public string? MutationName { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Bird> Birds { get; set; }
        public virtual ICollection<SpeciesMutation> SpeciesMutations { get; set; }
    }
}
