using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IBreedingCheckListRepository : IGenericRepository<BreedingCheckList>
    {
        BreedingCheckList GetBreedingCheckList(int breedingId, int phase);
        Task<IEnumerable<BreedingCheckList>> GetBreedingCheckListsByBreedingId(int breedingId);
        Task<IEnumerable<BreedingCheckList>> GetBreedingCheckListsByBreedingIdAndPhase(int breedingId, int phase);
        Task<IEnumerable<BreedingCheckList>> GetBreedingCheckListsByClutchIdAndPhase(int breedingId, int phase);
        Task<BreedingCheckList?> GetTodayCheckListByBreedingId(object breedingId);
        Task<BreedingCheckList?> GetTodayCheckListByBreedingIdAndPhase(object breedingId, int phase);
        Task<BreedingCheckList?> GetTodayCheckListByClutchIdAndPhase(object clutchId, int phase);

    }
}

