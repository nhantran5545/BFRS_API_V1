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
            var result = _birdRepository.SaveChanges();
            if(result < 1)
            {
                return result;
            }
            return bird.BirdId;
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

        public async Task<IEnumerable<BirdResponse>> GetInRestBirdsBySpeciesIdAndFarmId(object speciesId, object farmId)
        {
            var birds = await _birdRepository.GetInRestBirdsBySpeciesIdAndFarmId(speciesId, farmId);
            return birds.Select(b => _mapper.Map<BirdResponse>(b));
        }

        public async Task<BirdDetailResponse?> GetBirdByIdAsync(object birdId)
        {
            var bird = await _birdRepository.GetByIdAsync(birdId);
            return _mapper.Map<BirdDetailResponse>(bird);
        }

        public async Task<BirdDetailResponse?> GetBirdByEggIdAsync(object eggId)
        {
            var bird = await _birdRepository.GetByIdAsync(eggId);
            return _mapper.Map<BirdDetailResponse>(bird);
        }
        public async Task<Dictionary<string, BirdPedi>> GetPedigreeOfABird(int birdId)
        {
            var birdAlgorithmService = new BirdAlgorithmService(_birdRepository);
            Dictionary<string, BirdPedi> pairs = new Dictionary<string, BirdPedi>();
            var pedigree = await birdAlgorithmService.GetPedigree(birdId);
            foreach (var item in pedigree)
            {
                pairs.Add(item.Key, _mapper.Map<BirdPedi>(item.Value));
            }
            return pairs;
        }

        public async Task<bool> UpdateBirdAsync(BirdUpdateRequest birdUpdateRequest)
        {
            var bird = await _birdRepository.GetByIdAsync(birdUpdateRequest.BirdId);
            if(bird == null)
            {
                return false;
            }

            bird.Gender = birdUpdateRequest.Gender;
            bird.HatchedDate = birdUpdateRequest.HatchedDate;
            bird.PurchaseFrom = birdUpdateRequest.PurchaseFrom;
            bird.AcquisitionDate = birdUpdateRequest.AcquisitionDate;
            bird.BirdSpeciesId = birdUpdateRequest.BirdSpeciesId;
            bird.CageId = birdUpdateRequest.CageId;
            bird.FarmId = birdUpdateRequest.FarmId;
            bird.FatherBirdId = birdUpdateRequest.FatherBirdId;
            bird.MotherBirdId = birdUpdateRequest.MotherBirdId;
            bird.BandNumber = birdUpdateRequest.BandNumber;
            bird.Image = birdUpdateRequest.Image;
            bird.LifeStage = birdUpdateRequest.LifeStage;
            bird.Status = birdUpdateRequest.Status;

            var result = _birdRepository.SaveChanges();
            if(result < 1)
            {
                return false;
            }
            return true;
        }
    }
}
