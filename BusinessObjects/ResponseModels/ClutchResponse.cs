using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class ClutchResponse
    {
        public Guid ClutchId { get; set; }
        public Guid? BreedingId { get; set; }
        public DateTime? BroodStartDate { get; set; }
        public DateTime? BroodEndDate { get; set; }
        public Guid? CageId { get; set; }
        public string? Status { get; set; }
    }
}
