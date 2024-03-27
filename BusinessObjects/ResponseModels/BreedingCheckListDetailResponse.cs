using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class BreedingCheckListDetailResponse
    {
        public int BreedingCheckListDetailId { get; set; }
        public CheckListDetailResponse? CheckListDetailResponse { get; set; }
        public int BreedingCheckListId { get; set; }
        public int CheckValue { get; set; }
    }
}
