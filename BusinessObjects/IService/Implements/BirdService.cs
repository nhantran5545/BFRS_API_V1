using AutoMapper;
using BusinessObjects.RequestModels;
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
    public class BirdService : IBirdService
    {
        private readonly IBirdRepository _birdRepository;
        private readonly IMapper _mapper;

        public BirdService(IBirdRepository birdRepository, IMapper mapper)
        {
            _birdRepository = birdRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateBirdAsync(BirdAddRequest birdAddRequest)
        {
            var bird = _mapper.Map<Bird>(birdAddRequest);
            if (bird == null)
            {
                return -1;
            }
            await _birdRepository.AddAsync(bird);
            return _birdRepository.SaveChanges();
        }

        public void DeleteBird(Bird bird)
        {
            throw new NotImplementedException();
        }

        public void DeleteBirdById(object birdId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BirdResponse>> GetAllBirdsAsync()
        {
            var birds = await _birdRepository.GetAllAsync();
            return birds.Select(b => _mapper.Map<BirdResponse>(b));
        }

        public async Task<IEnumerable<BirdResponse>> GetBirdsByFarmId(object farmId)
        {
            var birds = await _birdRepository.GetBirdsByFarmId(farmId);
            return birds.Select(b => _mapper.Map<BirdResponse>(b));
        }

        public async Task<BirdDetailResponse?> GetBirdByIdAsync(object birdId)
        {
            var bird = await _birdRepository.GetByIdAsync(birdId);
            return _mapper.Map<BirdDetailResponse?>(bird);
        }

        public async Task<IEnumerable<BirdResponse>> GetInRestBirdsBySpeciesIdAndFarmId(object speciesId, object farmId)
        {
            var birds = await _birdRepository.GetInRestBirdsBySpeciesIdAndFarmId(speciesId, farmId);
            return birds.Select(b => _mapper.Map<BirdResponse>(b));
        }

        public void UpdateBird(Bird bird)
        {
            throw new NotImplementedException();
        }
    }
}
