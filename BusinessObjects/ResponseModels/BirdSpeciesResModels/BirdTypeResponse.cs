using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels.BirdSpeciesResModels
{
    public class BirdTypeResponse
    {
        public int BirdTypeId { get; set; }
        public string? BirdTypeName { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}
