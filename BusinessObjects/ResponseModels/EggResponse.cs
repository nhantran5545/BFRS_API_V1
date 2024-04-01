using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class EggResponse
    {
        public int EggId { get; set; }
        public int? ClutchId { get; set; }
        public DateTime? LayDate { get; set; }
        public DateTime? HatchedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string? Status { get; set; }
        public int? BirdId { get; set; }
        public int? BandNumber { get; set; }
    }
}
