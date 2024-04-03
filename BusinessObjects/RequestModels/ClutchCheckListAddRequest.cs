using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels
{
    public class ClutchCheckListAddRequest
    {
        public int ClutchId { get; set; }
        public int CheckListId { get; set; }
        public List<BreedingCheckListAddRequestDetail> BreedingCheckListAddRequestDetails { get; set; } = new List<BreedingCheckListAddRequestDetail>();
    }
}
