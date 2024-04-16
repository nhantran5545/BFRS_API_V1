using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels.TimelineResModels
{
    public class EggTimeline
    {
        public int EggId { get; set; }
        public List<StatusChangeResponse>? EggStatuses { get; set; }
    }
}
