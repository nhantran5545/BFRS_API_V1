using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels
{
    public class BreedingCloseRequest
    {
        public int BreedingId;
        public int ManagerId;
        public int FatherCageId;
        public int MotherCageId;
    }
}
