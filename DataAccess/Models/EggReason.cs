using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class EggReason
    {
        public int EggReasonId { get; set; }
        public int? EggId { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string? Status { get; set; }

        public virtual Account? CreatedByNavigation { get; set; }
        public virtual Egg? Egg { get; set; }
    }
}
