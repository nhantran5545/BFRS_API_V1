using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels.BreedingChecklistResModels
{
    public class BreedingCheckListResponse
    {
        public int BreedingCheckListId { get; set; }
        public int? BreedingId { get; set; }
        public int? ClutchId { get; set; }
        public int CheckListId { get; set; }
        public int Phase { get; set; }
        public DateTime? CreateDate { get; set; }
        public List<BreedingCheckListDetailResponse>? BreedingCheckListDetails { get; set; }
    }
}
