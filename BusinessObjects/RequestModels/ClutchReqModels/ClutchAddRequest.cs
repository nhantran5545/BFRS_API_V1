using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.ClutchReqModels
{
    public class ClutchAddRequest
    {
        public int BreedingId { get; set; }
        public DateTime? BroodStartDate { get; set; }
        //public int CreatedBy { get; set; }
    }
}
