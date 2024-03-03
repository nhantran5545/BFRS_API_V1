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

        public Task CreateClutchAsync(Clutch clutch)
        {
            throw new NotImplementedException();
        }

        public void DeleteClutch(Clutch clutch)
        {
            throw new NotImplementedException();
        }

        public void DeleteClutchtById(object clutchId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Clutch>> GetAllClutchsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Clutch>> GetAllClutchsByBreedingId(object breedingId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Clutch>> GetAllClutchsByCreatedById(object CreatedById)
        {
            throw new NotImplementedException();
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
