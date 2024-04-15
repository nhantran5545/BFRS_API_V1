using BusinessObjects.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface IReasonService
    {
        Task<IEnumerable<ReasonResponse>> GetStatusByBreedingId(object breedingId);
    }
}
