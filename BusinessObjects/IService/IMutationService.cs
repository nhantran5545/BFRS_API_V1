using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface IMutationService
    {
        Task<int> CreateMutationAsync(MutationRequest mutationRequest);
        Task<IEnumerable<IndividualMutation>> GetAllMutationsAsync();
        Task<IEnumerable<IndividualMutation>> GetMutationsBySpeciesIdAsync(int speciesId);
        Task<IndividualMutation?> GetMutationByIdAsync(object mutationId);
    }
}
