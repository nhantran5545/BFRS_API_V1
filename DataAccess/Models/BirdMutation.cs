using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class BirdMutation
    {
        public int BirdId { get; set; }
        public int MutationId { get; set; }
        public string? Status { get; set; }

        public virtual Bird Bird { get; set; } = null!;
        public virtual Mutation Mutation { get; set; } = null!;
    }
}
