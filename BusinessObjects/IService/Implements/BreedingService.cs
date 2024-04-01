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
        private readonly IAccountRepository _accountRepository;
        private readonly ICageRepository _cageRepository;
        private readonly IBirdSpeciesRepository _birdSpeciesRepository;
        private readonly IMapper _mapper;

        public BreedingService(IBreedingRepository breedingRepository, IBirdRepository birdRepository,
            ICageRepository cageRepository, IBirdSpeciesRepository birdSpeciesRepository, IMapper mapper, IAccountRepository accountRepository)
        {
            _breedingRepository = breedingRepository;
            _birdRepository = birdRepository;
            _cageRepository = cageRepository;
            _birdSpeciesRepository = birdSpeciesRepository;
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        public async Task<double> CalculateInbreedingPercentage(int fatherBirdId, int motherBirdId)
        {
            var birdAlgorithmService = new BirdAlgorithmService(_birdRepository);
            var InbreedingPercentage = await birdAlgorithmService.GetInbreedingCoefficientOfParentsAsync(fatherBirdId, motherBirdId);
            return InbreedingPercentage;
        }

        public async Task<int> CreateBreeding(BreedingAddRequest breedingAddRequest)
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
                    breeding.CreatedBy = breedingAddRequest.ManagerId;
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
            return breedings.Select(br => _mapper.Map<BreedingResponse>(br));
        }

        public async Task<IEnumerable<BreedingResponse>> GetAllBreedingsByStaffId(object staffId)
        {
            var breedings = await _breedingRepository.GetAllBreedingsByStaffId(staffId);
            return breedings.Select(br => _mapper.Map<BreedingResponse>(br));
        }

        public async Task<IEnumerable<BreedingResponse>> GetBreedingsByStaffIdAsync(int staffId)
        {
            var account = await _accountRepository.GetByIdAsync(staffId);
            if (account == null)
            {
                throw new Exception("Invalid staff ID");
            }

            if (account.Role != "Staff")
            {
                throw new Exception("The provided account is not a staff member");
            }

            var breedings = await _breedingRepository.GetAllBreedingsByStaffId(staffId);
            return breedings.Select(br => _mapper.Map<BreedingResponse>(br));
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

        /*public async Task<bool> PutBirdsToBreeding(BreedingUpdateRequest breedingUpdateRequest)
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

                    breeding.CageId = null;
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
        }*/

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

        public async Task<bool> CancelBreeding(BreedingUpdateRequest breedingUpdateRequest)
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

                    breeding.CoupleSeperated = true;
                    breeding.Status = breedingUpdateRequest.Status;
                    breeding.UpdatedBy = breedingUpdateRequest.ManagerId;
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
    }
}
