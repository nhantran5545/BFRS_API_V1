using AutoMapper;
using BusinessObjects.ResponseModels;
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
        private readonly IMapper _mapper;

        public ClutchService(IClutchRepository clutchRepository, IMapper mapper)
        {
            _clutchRepository = clutchRepository;
            _mapper = mapper;
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

        public async Task<IEnumerable<ClutchResponse>> GetAllClutchsAsync()
        {
            var clutchs = await _clutchRepository.GetAllAsync();
            return clutchs.Select(c => _mapper.Map<ClutchResponse>(c));
        }

        public async Task<IEnumerable<ClutchResponse>> GetAllClutchsByBreedingId(object breedingId)
        {
            var clutchs = await _clutchRepository.GetAllClutchsByBreedingId(breedingId);
            return clutchs.Select(c => _mapper.Map<ClutchResponse>(c));
        }

        public async Task<IEnumerable<ClutchResponse>> GetAllClutchsByCreatedById(object CreatedById)
        {
            var clutchs = await _clutchRepository.GetAllClutchsByCreatedById(CreatedById);
            return clutchs.Select(c => _mapper.Map<ClutchResponse>(c));
        }

        public async Task<ClutchDetailResponse?> GetClutchByIdAsync(object clutchId)
        {
            var clutch = await _clutchRepository.GetByIdAsync(clutchId);
            return _mapper.Map<ClutchDetailResponse>(clutch);
        }

        public void UpdateClutch(Clutch clutch)
        {
            throw new NotImplementedException();
        }
    }
}
