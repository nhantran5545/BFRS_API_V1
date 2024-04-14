using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class EggStatusChange
    {
        public int EggChangeId { get; set; }
        public int? EggId { get; set; }
        public string? Description { get; set; }
        public DateTime? ChangedDate { get; set; }
        public int? ChangedBy { get; set; }
        public string? OldStatus { get; set; }
        public string? NewStatus { get; set; }

        public virtual Account? ChangedByNavigation { get; set; }
        public virtual Egg? Egg { get; set; }
    }
}
