using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.BreedingReqModels
{
    public class BreedingAddRequest
    {
        public int FatherBirdId { get; set; }
        public int MotherBirdId { get; set; }
        public int CageId { get; set; }
        //public int ManagerId { get; set; }
    }
}
