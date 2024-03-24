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
        private readonly IMapper _mapper;

        public BreedingService(IBreedingRepository breedingRepository, IBirdRepository birdRepository, 
            ICageRepository cageRepository, IMapper mapper)
        {
            _breedingRepository = breedingRepository;
            _birdRepository = birdRepository;
            _cageRepository = cageRepository;
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
            var breeding = new Breeding
            {
                MotherBirdId = breedingAddRequest.MotherBirdId,
                FatherBirdId = breedingAddRequest.FatherBirdId,
                CageId = breedingAddRequest.CageId,
            };
            if(breeding == null)
            {
                return -1;
            }
            breeding.CoupleSeperated = true;
            breeding.Status = "Openned";
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
            return breedings.Select(br => new BreedingResponse
            {
                BreedingId = br.BreedingId,
                FatherBirdId = br.FatherBirdId.HasValue ? br.FatherBirdId.Value : 0,
                MotherBirdId = br.MotherBirdId.HasValue ? br.MotherBirdId.Value : 0,
                CoupleSeperated = br.CoupleSeperated,
                CageId = br.CageId.HasValue ? br.CageId.Value : 0,
                NextCheck = br.NextCheck
            });
        }

        public async Task<IEnumerable<BreedingResponse>> GetAllBreedingsByManagerId(object managerId)
        {
            var breedings = await _breedingRepository.GetAllBreedingsByManagerId(managerId);
            return breedings.Select(br => new BreedingResponse
            {
                BreedingId = br.BreedingId,
                FatherBirdId = br.FatherBirdId.HasValue ? br.FatherBirdId.Value : 0,
                MotherBirdId = br.MotherBirdId.HasValue ? br.MotherBirdId.Value : 0,
                CoupleSeperated = br.CoupleSeperated,
                CageId = br.CageId.HasValue ? br.CageId.Value : 0,
                NextCheck = br.NextCheck
            });
        }


        public async Task<BreedingDetailResponse?> GetBreedingById(object breedingId)
        {
            var breeding = await _breedingRepository.GetByIdAsync(breedingId);
            if (breeding == null)
            {
                return null;
            }

            return new BreedingDetailResponse
            {
                BreedingId = breeding.BreedingId,
                FatherBirdId = breeding.FatherBirdId.HasValue ? breeding.FatherBirdId.Value : 0,
                MotherBirdId = breeding.MotherBirdId.HasValue ? breeding.MotherBirdId.Value : 0,
                CoupleSeperated = breeding.CoupleSeperated,
                CageId = breeding.CageId.HasValue ? breeding.CageId.Value : 0,
                NextCheck = breeding.NextCheck,
                CreatedDate = breeding.CreatedDate,
                UpdatedDate = breeding.UpdatedDate,
                UpdatedBy = breeding.UpdatedBy.HasValue ? breeding.UpdatedBy.Value : 0,
                Status = breeding.Status
            };
        }


        public async Task<bool> PutBirdsToBreeding(BreedingUpdateRequest breedingUpdateRequest)
        {
            using (var transaction = _breedingRepository.BeginTransaction())
            {
                try
                {
                    var breeding = await _breedingRepository.GetByIdAsync(breedingUpdateRequest.BreedingId);
                    if (breeding == null || breeding.Status != "Openned")
                    {
                        return false;
                    }
                    breeding.Status = "Mating";
                    breeding.UpdatedBy = breedingUpdateRequest.StaffId;
                    breeding.UpdatedDate = DateTime.Now;
                    _breedingRepository.SaveChanges();

                    var fatherBird = await _birdRepository.GetByIdAsync(breeding.FatherBirdId);
                    fatherBird.CageId = breeding.CageId;
                    var motherBird = await _birdRepository.GetByIdAsync(breeding.MotherBirdId);
                    motherBird.CageId = breeding.CageId;
                    var cage = await _cageRepository.GetByIdAsync(breeding.CageId);
                    cage.Status = "Breeding";
                    _breedingRepository.SaveChanges();

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
                    breeding.CoupleSeperated = false;
                    breeding.Status = "Closed";
                    breeding.UpdatedBy = breedingCloseRequest.ManagerId;
                    breeding.UpdatedDate = DateTime.Now;
                    _breedingRepository.SaveChanges();

                    bool flag = false;
                    if (breeding.FatherBirdId != null && breedingCloseRequest.FatherCageId != null)
                    {
                        var fatherBird = await _birdRepository.GetByIdAsync(breeding.FatherBirdId);
                        if (fatherBird != null && fatherBird.CageId != breedingCloseRequest.FatherCageId)
                        {
                            fatherBird.CageId = breedingCloseRequest.FatherCageId;
                            var fatherCage = await _cageRepository.GetByIdAsync(breedingCloseRequest.FatherCageId);
                            if(fatherCage != null)
                            {
                                fatherCage.Status = "Nourishing";
                            }
                            flag = true;
                        }
                    }

                    if (breeding.MotherBirdId != null && breedingCloseRequest.MotherCageId != null)
                    {
                        var motherBird = await _birdRepository.GetByIdAsync(breeding.MotherBirdId);
                        if (motherBird != null && motherBird.CageId != breedingCloseRequest.MotherCageId)
                        {
                            motherBird.CageId = breedingCloseRequest.MotherCageId;
                            var motherCage = await _cageRepository.GetByIdAsync(breedingCloseRequest.MotherCageId);
                            if (motherCage != null)
                            {
                                motherCage.Status = "Nourishing";
                            }
                            flag = true;
                        }
                    }

                    if (flag)
                    {
                        _birdRepository.SaveChanges();
                    }

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
