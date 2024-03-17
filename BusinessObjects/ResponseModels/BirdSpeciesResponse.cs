using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class BirdSpeciesResponse
    {
        public Guid BirdSpeciesId { get; set; }
        public string? BirdSpeciesName { get; set; }
        public Guid? BirdTypeId { get; set; }
        public string? BirdTypeName { get; set; }
    }
}
