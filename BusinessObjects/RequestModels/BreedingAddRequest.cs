using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels
{
    public class BreedingAddRequest
    {
        public Guid? FatherBirdId { get; set; }
        public Guid? MotherBirdId { get; set; }
        public Guid? CageId { get; set; }
    }
}
