using BusinessObjects.ResponseModels.TimelineResModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface IReasonService
    {
        Task<BreedingTimeline?> GetTimelineByBreedingId(int breedingId);
    }
}
