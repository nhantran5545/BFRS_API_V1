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
        Task CreateBirdSpeciesAsync(BirdSpecy birdSpecy);
        void UpdateBirdSpecies(BirdSpecy birdSpecy);
        void DeleteBirdSpecies(BirdSpecy birdSpecy);
        Task<IEnumerable<BirdSpecy>> GetBirdSpeciesAsync();
        Task<BirdSpecy> GetBirdSpeciesByIdAsync(object BirdSpecyId);
    }
}
