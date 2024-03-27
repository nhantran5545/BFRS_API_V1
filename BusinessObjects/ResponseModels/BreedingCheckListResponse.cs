using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class BreedingCheckListResponse
    {
        public int BreedingCheckListId { get; set; }
        public int? BreedingId { get; set; }
        public int Phase { get; set; }
        public List<BreedingCheckListDetailResponse>? BreedingCheckListDetails { get; set; }
    }
}
