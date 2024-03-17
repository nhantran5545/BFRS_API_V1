using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class EggResponse
    {
        public Guid EggId { get; set; }
        public Guid? ClutchId { get; set; }
        public DateTime? LayDate { get; set; }
        public DateTime? HatchedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? Status { get; set; }
    }
}
