using DataAccess.IRepositories;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService.Implements
{
    public class ClutchService : IClutchService
    {
        private readonly IClutchRepository _clutchRepository;

        public ClutchService(IClutchRepository clutchRepository)
        {
            _clutchRepository = clutchRepository;
        }

        public async Task CreateClutchAsync(Clutch clutch)
        {
            await _clutchRepository.AddAsync(clutch);
            _clutchRepository.SaveChanges();
        }

        public void DeleteClutch(Clutch clutch)
        {
            throw new NotImplementedException();
        }

        public void DeleteClutchtById(object clutchId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Clutch>> GetAllClutchsAsync()
        {
            return await _clutchRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Clutch>> GetAllClutchsByBreedingId(object breedingId)
        {
            return await _clutchRepository.GetAllClutchsByBreedingId(breedingId);
        }

        public async Task<IEnumerable<Clutch>> GetAllClutchsByCreatedById(object CreatedById)
        {
            return await _clutchRepository.GetAllClutchsByCreatedById(CreatedById);
        }

        public Task<Clutch?> GetClutchByIdAsync(object clutchId)
        {
            throw new NotImplementedException();
        }

        public void UpdateClutch(Clutch clutch)
        {
            throw new NotImplementedException();
        }
    }
}
