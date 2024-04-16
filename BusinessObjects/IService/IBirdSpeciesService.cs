using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels.BirdSpeciesResModels;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface IBirdSpeciesService
    {
        Task<int> CreateBirdSpeciesAsync(BirdSpeciesRequest birdSpecy);
        Task<bool> UpdateSpeciesAsync(int BirdSpecyId, BirdSpeciesRequest request);
        void DeleteBirdSpecies(BirdSpecy birdSpecy);
        Task<IEnumerable<BirdSpeciesResponse>> GetBirdSpeciesAsync();
        Task<BirdSpeciesDetailResponse?> GetBirdSpeciesByIdAsync(object BirdSpecyId);
        Task<IEnumerable<BirdTypeResponse>> GetBirdTypes();
    }
}
