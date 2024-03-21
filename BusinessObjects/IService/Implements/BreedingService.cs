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
    public class BreedingService : IBreedingService
    {
        private readonly IBreedingRepository _breedingRepository;
        private readonly IBirdRepository _birdRepository;
        private readonly IMapper _mapper;

        public BreedingService(IBreedingRepository breedingRepository, IBirdRepository birdRepository, IMapper mapper)
        {
            _breedingRepository = breedingRepository;
            _birdRepository = birdRepository;
            _mapper = mapper;
        }

        public async Task<double> CalculateInbreedingPercentage(int fatherBirdId, int motherBirdId)
        {
            var birdAlgorithmService = new BirdAlgorithmService(_birdRepository);
            var InbreedingPercentage = await birdAlgorithmService.GetInbreedingCoefficientOfParentsAsync(fatherBirdId, motherBirdId);
            return InbreedingPercentage;
        }

        public async Task<int> CreateBreeding(BreedingAddRequest breedingAddRequest)
        {
            var breeding = _mapper.Map<Breeding>(breedingAddRequest);
            if(breeding == null)
            {
                return -1;
            }
            breeding.Status = "Openned";
            await _breedingRepository.AddAsync(breeding);
            var result = _breedingRepository.SaveChanges();
            if(result < 1)
            {
                return result;
            }
            return breeding.BreedingId;
        }

        public void DeleteBreeding(BreedingAddRequest breeding)
        {
            throw new NotImplementedException();
        }

        public void DeleteBreedingById(object breedingId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BreedingResponse>> GetAllBreedings()
        {
            var breedings = await _breedingRepository.GetAllAsync();
            return breedings.Select(br => _mapper.Map<BreedingResponse>(br));
        }

        public async Task<IEnumerable<BreedingResponse>> GetAllBreedingsByManagerId(object managerId)
        {
            var breedings = await _breedingRepository.GetAllBreedingsByManagerId(managerId);
            return breedings.Select(br => _mapper.Map<BreedingResponse>(br));
        }

        public async Task<BreedingDetailResponse?> GetBreedingById(object breedingId)
        {
            var breeding = await _breedingRepository.GetByIdAsync(breedingId);
            return _mapper.Map<BreedingDetailResponse>(breeding);
        }

        public async Task<bool> PutBirdsToBreeding(BreedingUpdateRequest breedingUpdateRequest)
        {
            var breeding = await _breedingRepository.GetByIdAsync(breedingUpdateRequest.BreedingId);
            if(breeding == null || breeding.Status != "Openned")
            {
                return false;
            }
            breeding.Status = "Mating";
            breeding.UpdatedBy = breedingUpdateRequest.StaffId;
            breeding.UpdatedDate = DateTime.Now;
            int result = _breedingRepository.SaveChanges();
            if(result < 1)
            {
                return false;
            }
            var fatherBird = await _birdRepository.GetByIdAsync(breeding.FatherBirdId);
            fatherBird.CageId = breeding.CageId;
            var motherBird = await _birdRepository.GetByIdAsync(breeding.MotherBirdId);
            motherBird.CageId = breeding.CageId;
            result = _breedingRepository.SaveChanges(); 
            if(result < 1)
            {
                return false;
            }
            return true;
        }

        public void UpdateBreeding(BreedingAddRequest breeding)
        {
            throw new NotImplementedException();
        }
    }
}
