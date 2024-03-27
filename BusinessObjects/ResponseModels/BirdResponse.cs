using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class BirdResponse
    {
        public int BirdId { get; set; }
        public int? BandNumber { get; set; }
        public string? Gender { get; set; }
        public DateTime? HatchedDate { get; set; }
        public int CageId { get; set; }
        public int FarmId { get; set; }
        public string? FarmName { get; set; }
        public int BirdSpeciesId { get; set; }
        public string? BirdSpeciesName { get; set; }
        public string? Image { get; set; }
        public string? Status { get; set; }
    }
}
