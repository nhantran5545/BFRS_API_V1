using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels.TimelineResModels
{
    public class BreedingTimeline
    {
        public int? BreedingId { get; set; }
        public List<StatusChangeResponse>? BreedingStatuses { get; set; }
        public List<ClutchTimeline>? ClutchTimelines { get; set; }

    }
}
