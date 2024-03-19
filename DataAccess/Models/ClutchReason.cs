using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class ClutchReason
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ClutchReasonId { get; set; }
        public Guid? ClutchId { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public string? Status { get; set; }

        public virtual Clutch? Clutch { get; set; }
        public virtual Account? CreatedByNavigation { get; set; }
    }
}
