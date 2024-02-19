using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class SpeciesMutation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BirdSpeciesId { get; set; }
        public Guid MutationId { get; set; }
        public string? Status { get; set; }

        public virtual BirdSpecy BirdSpecies { get; set; } = null!;
        public virtual Mutation Mutation { get; set; } = null!;
    }
}
