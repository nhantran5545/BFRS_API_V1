using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels
{
    public class BreedingCloseRequest
    {
        public int BreedingId { get; set; }
        public int ManagerId { get; set; }
        public int FatherCageId { get; set; }
        public int MotherCageId { get; set; }
    }
}
