using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class BirdReponse
    {
        public Guid BirdId { get; set; }
        public string? Sex { get; set; }
        public DateTime? HatchedDate { get; set; }
        public Guid? CageId { get; set; }
        public Guid? BirdSpeciesId { get; set; }
        public string? BirdSpeciesName { get; set; }
        public string? Image { get; set; }
        public string? Status { get; set; }
    }
}
