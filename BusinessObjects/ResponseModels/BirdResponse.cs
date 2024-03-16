using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class BirdResponse
    {
        public Guid BirdId { get; set; }
        public string? Gender { get; set; }
        public DateTime? HatchedDate { get; set; }
        public Guid? CageId { get; set; }
        public Guid? BirdSpeciesId { get; set; }
        public string? BirdSpeciesName { get; set; }
        public string? Image { get; set; }
        public string? Status { get; set; }
    }
}
