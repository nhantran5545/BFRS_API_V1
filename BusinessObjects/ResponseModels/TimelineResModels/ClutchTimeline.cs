using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels.TimelineResModels
{
    public class ClutchTimeline
    {
        public int? ClutchId { get; set; }
        public List<StatusChangeResponse>? ClutchStatuses { get; set; }
        public List<EggTimeline>? EggTimelines { get; set; }
    }
}
