using AutoMapper;
using BusinessObjects.RequestModels.BreedingReqModels;
using BusinessObjects.ResponseModels.BreedingResModels;
using BusinessObjects.ResponseModels.ClutchResModels;
using DataAccess.IRepositories;
using DataAccess.IRepositories.Implements;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace BusinessObjects.IService.Implements
{
    public class BreedingService : IBreedingService
    {
        private readonly IBreedingRepository _breedingRepository;
        private readonly IBirdRepository _birdRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICageRepository _cageRepository;
        private readonly IBirdSpeciesRepository _birdSpeciesRepository;
        private readonly IClutchRepository _clutchRepository;
        private readonly IStatusChangeService _statusChangeService;
        private readonly IMapper _mapper;

        public BreedingService(IBreedingRepository breedingRepository, IBirdRepository birdRepository,
            ICageRepository cageRepository, IBirdSpeciesRepository birdSpeciesRepository, IClutchRepository clutchRepository,
            IStatusChangeService statusChangeService, IMapper mapper, IAccountRepository accountRepository)
        {
            _breedingRepository = breedingRepository;
            _birdRepository = birdRepository;
            _cageRepository = cageRepository;
            _birdSpeciesRepository = birdSpeciesRepository;
            _clutchRepository = clutchRepository;
            _statusChangeService = statusChangeService;
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        public async Task<double> CalculateInbreedingPercentage(int fatherBirdId, int motherBirdId)
        {
            var birdAlgorithmService = new BirdAlgorithmService(_birdRepository);
            var InbreedingPercentage = await birdAlgorithmService.GetInbreedingCoefficientOfParentsAsync(fatherBirdId, motherBirdId);
            return InbreedingPercentage;
        }

        public async Task<int> CreateBreeding(BreedingAddRequest breedingAddRequest, int managerId)
        {
            using (var transaction = _breedingRepository.BeginTransaction())
            {
                try
                {
                    var breeding = _mapper.Map<Breeding>(breedingAddRequest);
                    if (breeding == null)
                    {
                        return -1;
                    }

                    breeding.CoupleSeperated = false;
                    breeding.Status = "Mating";
                    breeding.Phase = 1;
                    breeding.CreatedBy = managerId;
                    breeding.CreatedDate = DateTime.Now;
                    await _breedingRepository.AddAsync(breeding);
                    var result = _breedingRepository.SaveChanges();
                    if (result < 1)
                    {
                        return result;
                    }

                    var fatherBird = await _birdRepository.GetByIdAsync(breedingAddRequest.FatherBirdId);
                    if (fatherBird != null)//fatherBird never null
                    {
                        fatherBird.CageId = breeding.CageId;
                        fatherBird.Status = "InReproductionPeriod";
                    }
                    
                    var motherBird = await _birdRepository.GetByIdAsync(breedingAddRequest.MotherBirdId);
                    if (motherBird != null)//motherBird never null
                    {
                        motherBird.CageId = breeding.CageId;
                        motherBird.Status = "InReproductionPeriod";
                    }
                    
                    _birdRepository.SaveChanges();

                    var cage = await _cageRepository.GetByIdAsync(breedingAddRequest.CageId);
                    if(cage != null)//Cage never null
                    {
                        cage.Status = "Breeding";
                    }
                    
                    _cageRepository.SaveChanges();

                    await _statusChangeService.AddBreedingChangeStatus(breeding.BreedingId, null, managerId, null, breeding.Status);

                    transaction.Commit();
                    return breeding.BreedingId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                    return -1;
                }
            }

        }

        public async Task<IEnumerable<BreedingResponse>> GetAllBreedings()
        {
            var breedings = await _breedingRepository.GetAllAsync();
            List<BreedingResponse> breedingResponses = new List<BreedingResponse>();
            if (breedings.Any())
            {
                foreach (var item in breedings)
                {
                    var breedingResponse = _mapper.Map<BreedingResponse>(item);
                    var species = await _birdSpeciesRepository.GetByIdAsync(breedingResponse.SpeciesId);
                    if (species != null)
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
            List<BreedingResponse> breedingResponses = new List<BreedingResponse>();
            if (breedings.Any())
            {
                foreach (var item in breedings)
                {
                    var breedingResponse = _mapper.Map<BreedingResponse>(item);
                    var species = await _birdSpeciesRepository.GetByIdAsync(breedingResponse.SpeciesId);
                    if (species != null)
                    {
                        breedingResponse.SpeciesName = species.BirdSpeciesName;
                    }
                    breedingResponses.Add(breedingResponse);
                }
            }
            return breedingResponses;
        }


        public async Task<IEnumerable<BreedingResponse>> GetBreedingsByStaffIdAsync(int staffId)
        {

            var breedings = await _breedingRepository.GetAllBreedingsByStaff(staffId);
            return breedings.Select(br => _mapper.Map<BreedingResponse>(br));
        }

        public async Task<int> GetBreedingCountByStatusNameAndManagedByStaff(int staffId,string status)
        {
            return await _breedingRepository.GetTotalStatusBreedingsByStaff(staffId, status);
        }
        public async Task<int> GetBreedingCountByStatusNameAndManagedByManager(int managerId, string status)
        {
            return await _breedingRepository.GetTotalStatusBreedingsByManagerId(managerId, status);
        }

        public async Task<int> GetTotalBreedingCountByManagerId(int managerId)
        {
            var breedings = await _breedingRepository.GetAllBreedingsByManagerId(managerId);
            return breedings.Count();
        }


        public async Task<BreedingDetailResponse?> GetBreedingById(object breedingId)
        {
            var breeding = await _breedingRepository.GetByIdAsync(breedingId);
            if (breeding == null)
            {
                return null;
            }

            var breedingResponse = _mapper.Map<BreedingDetailResponse>(breeding);
            var species = await _birdSpeciesRepository.GetByIdAsync(breedingResponse.SpeciesId);
            if (species != null)
            {
                breedingResponse.SpeciesName = species.BirdSpeciesName;
            }

            breedingResponse.ClutchResponses = breeding.Clutches.Select(c => _mapper.Map<ClutchResponse>(c)).ToList();
            return breedingResponse;
        }

        public async Task<bool> CloseBreeding(BreedingCloseRequest breedingCloseRequest, int managerId)
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

                    var oldStatus = breeding.Status;

                    breeding.CoupleSeperated = true;
                    breeding.Status = "Closed";
                    breeding.Phase = 0;
                    breeding.UpdatedBy = managerId;
                    breeding.UpdatedDate = DateTime.Now;
                    _breedingRepository.SaveChanges();

                    var fatherBird = await _birdRepository.GetByIdAsync(breeding.FatherBirdId);
                    if (fatherBird != null)//fatherBird never null
                    {
                        fatherBird.CageId = breedingCloseRequest.FatherCageId;
                        fatherBird.Status = "InRestPeriod";
                    }
                    var motherBird = await _birdRepository.GetByIdAsync(breeding.MotherBirdId);
                    if (motherBird != null)//motherBird never null
                    {
                        motherBird.CageId = breedingCloseRequest.MotherCageId;
                        motherBird.Status = "InRestPeriod";
                    }

                    _birdRepository.SaveChanges();

                    var cage = await _cageRepository.GetByIdAsync(breeding.CageId);
                    if (cage != null)//cage never null
                    {
                        cage.Status = "Standby";
                    }

                    _cageRepository.SaveChanges();

                    await _statusChangeService.AddBreedingChangeStatus(breeding.BreedingId, null, managerId, oldStatus, breeding.Status);

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> CancelBreeding(BreedingUpdateRequest breedingUpdateRequest, int managerId)
        {
            using (var transaction = _breedingRepository.BeginTransaction())
            {
                try
                {
                    var breeding = await _breedingRepository.GetByIdAsync(breedingUpdateRequest.BreedingId);
                    if (breeding == null)
                    {
                        return false;
                    }

                    await CloseClutchesByBreedingId(breeding.BreedingId);

                    var oldStatus = breeding.Status;
                    if(breeding.Status == "Mating")
                    {
                        breeding.Status = "Failed";
                    }
                    else if(breeding.Status == "InProgress")
                    {
                        breeding.Status = "Cancelled";
                    }
                    else
                    {
                        return false;
                    }

                    breeding.CoupleSeperated = true;
                    breeding.Phase = 0;
                    breeding.UpdatedBy = managerId;
                    breeding.UpdatedDate = DateTime.Now;
                    _breedingRepository.SaveChanges();

                    var fatherBird = await _birdRepository.GetByIdAsync(breeding.FatherBirdId);
                    if (fatherBird != null)//fatherBird never null
                    {
                        fatherBird.CageId = breedingUpdateRequest.FatherCageId;
                        fatherBird.Status = "InRestPeriod";
                    }
                    var motherBird = await _birdRepository.GetByIdAsync(breeding.MotherBirdId);
                    if (motherBird != null)//motherBird never null
                    {
                        motherBird.CageId = breedingUpdateRequest.MotherCageId;
                        motherBird.Status = "InRestPeriod";
                    }

                    _birdRepository.SaveChanges();

                    var cage = await _cageRepository.GetByIdAsync(breeding.CageId);
                    if (cage != null)//cage never null
                    {
                        cage.Status = "Standby";
                    }

                    _cageRepository.SaveChanges();

                    await _statusChangeService.AddBreedingChangeStatus(breeding.BreedingId, breedingUpdateRequest.Reason, managerId, oldStatus, breeding.Status);

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                    return false;
                }
            }
        }

        private async Task CloseClutchesByBreedingId(int breedingId)
        {
            var clutches = await _clutchRepository.GetClutchsByBreedingId(breedingId);
            if(clutches.Any())
            {
                foreach (var item in clutches)
                {
                    item.Status = "Closed";
                    item.Phase = 0;
                }
                _clutchRepository.SaveChanges();
            }
        }
    }
}
