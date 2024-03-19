using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class EggBird
    {
        public int EggId { get; set; }
        public int BirdId { get; set; }
        public string? Status { get; set; }

        public virtual Bird Bird { get; set; } = null!;
        public virtual Egg Egg { get; set; } = null!;
    }
}
