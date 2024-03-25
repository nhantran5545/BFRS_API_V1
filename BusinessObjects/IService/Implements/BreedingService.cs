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
        private readonly ICageRepository _cageRepository;
        private readonly IBirdSpeciesRepository _birdSpeciesRepository;
        private readonly IMapper _mapper;

        public BreedingService(IBreedingRepository breedingRepository, IBirdRepository birdRepository, 
            ICageRepository cageRepository, IBirdSpeciesRepository birdSpeciesRepository, IMapper mapper)
        {
            _breedingRepository = breedingRepository;
            _birdRepository = birdRepository;
            _cageRepository = cageRepository;
            _birdSpeciesRepository = birdSpeciesRepository;
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

            breeding.CoupleSeperated = true;
            breeding.Status = "Opened";
            breeding.CreatedBy = breedingAddRequest.ManagerId;
            breeding.CreatedDate = DateTime.Now;
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
            List<BreedingResponse> breedingResponses = new List<BreedingResponse>();
            if(breedings.Any())
            {
                foreach (var item in breedings)
                {
                    var breedingResponse = _mapper.Map<BreedingResponse>(item);
                    var species = await _birdSpeciesRepository.GetByIdAsync(breedingResponse.SpeciesId);
                    if(species != null)
                    {
                        breedingResponse.SpeciesName = species.BirdSpeciesName;
                    }
                    breedingResponses.Add(breedingResponse);
                }
            }
            return breedingResponses;
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
            using (var transaction = _breedingRepository.BeginTransaction())
            {
                try
                {
                    var breeding = await _breedingRepository.GetByIdAsync(breedingUpdateRequest.BreedingId);
                    if (breeding == null || breeding.Status != "Opened")
                    {
                        return false;
                    }
                    breeding.CoupleSeperated = false;
                    breeding.Status = "Mating";
                    breeding.UpdatedBy = breedingUpdateRequest.StaffId;
                    breeding.UpdatedDate = DateTime.Now;
                    _breedingRepository.SaveChanges();

                    var fatherBird = await _birdRepository.GetByIdAsync(breeding.FatherBirdId);
                    fatherBird.CageId = breeding.CageId;
                    fatherBird.Status = "InReproductionPeriod";
                    var motherBird = await _birdRepository.GetByIdAsync(breeding.MotherBirdId);
                    motherBird.CageId = breeding.CageId;
                    motherBird.Status = "InReproductionPeriod";
                    _birdRepository.SaveChanges();

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); 
                    return false;
                }
            }
        }
        public async Task<bool> BreedingInProgress(BreedingUpdateRequest breedingUpdateRequest)
        {
            var breeding = await _breedingRepository.GetByIdAsync(breedingUpdateRequest.BreedingId);
            if (breeding == null || breeding.Status != "Mating")
            {
                return false;
            }
            breeding.Status = "InProgress";
            breeding.UpdatedBy = breedingUpdateRequest.StaffId;
            breeding.UpdatedDate = DateTime.Now;
            int result = _breedingRepository.SaveChanges();
            if (result < 1)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CloseBreeding(BreedingCloseRequest breedingCloseRequest)
        {
            using (var transaction = _breedingRepository.BeginTransaction())
            {
                try
                {
                    var breeding = await _breedingRepository.GetByIdAsync(breedingCloseRequest.BreedingId);
                    if (breeding == null || breeding.Status != "InProgress")
                    {
                        return false;
                    }
                    breeding.CoupleSeperated = true;
                    breeding.Status = "Closed";
                    breeding.UpdatedBy = breedingCloseRequest.ManagerId;
                    breeding.UpdatedDate = DateTime.Now;
                    _breedingRepository.SaveChanges();

                    var fatherBird = await _birdRepository.GetByIdAsync(breeding.FatherBirdId);
                    fatherBird.CageId = breedingCloseRequest.FatherCageId;
                    var motherBird = await _birdRepository.GetByIdAsync(breeding.MotherBirdId);
                    motherBird.CageId = breedingCloseRequest.MotherCageId;
                    _birdRepository.SaveChanges();

                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback(); 
                    return false;
                }
            }
        }

        public void UpdateBreeding(BreedingAddRequest breeding)
        {
            throw new NotImplementedException();
        }
    }
}
