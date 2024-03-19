using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class EggReason
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EggReasonId { get; set; }
        public Guid? EggId { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public string? Status { get; set; }

        public virtual Account? CreatedByNavigation { get; set; }
        public virtual Egg? Egg { get; set; }
    }
}
