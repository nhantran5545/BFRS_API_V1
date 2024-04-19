using AutoMapper;
using BusinessObjects.RequestModels;
using BusinessObjects.RequestModels.BirdReqModels;
using BusinessObjects.ResponseModels;
using BusinessObjects.ResponseModels.BirdResModels;
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
        private readonly IEggRepository _eggRepository;
        private readonly IEggBirdRepository _eggBirdRepository;
        private readonly IBirdMutationRepository _birdMutationRepository;
        private readonly ICageRepository _cageRepository;
        private readonly IMapper _mapper;

        public BirdService(IBirdRepository birdRepository, IEggBirdRepository eggBirdRepository,
            IBirdMutationRepository birdMutationRepository, ICageRepository cageRepository,
            IMapper mapper, IEggRepository eggRepository)
        {
            _birdRepository = birdRepository;
            _eggBirdRepository = eggBirdRepository;
            _birdMutationRepository = birdMutationRepository;
            _mapper = mapper;
            _eggRepository = eggRepository;
            _cageRepository = cageRepository;
        }

        public async Task<int> CreateBirdAsync(BirdAddRequest birdAddRequest)
        {
            using (var transaction = _birdRepository.BeginTransaction())
            {
                try
                {
                    var bird = _mapper.Map<Bird>(birdAddRequest);
                    if (bird == null)
                    {
                        return -1;
                    }

                    var cage = await _cageRepository.GetByIdAsync(birdAddRequest.CageId);
                    if(cage != null && cage.Area != null)//cage never null
                    {
                        bird.FarmId = cage.Area.FarmId;
                    }

                    await _birdRepository.AddAsync(bird);
                    _birdRepository.SaveChanges();

                    if(birdAddRequest.MutationRequests != null && birdAddRequest.MutationRequests.Any())
                    {
                        await AddMutation(birdAddRequest.MutationRequests, bird.BirdId);
                    }

                    transaction.Commit();
                    return bird.BirdId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                    return -1;
                }
            }
        }

        public async Task<int> CreateBirdFromEggAsync(BirdAddFromEggRequest birdAddFromEggRequest)
        {
            using (var transaction = _birdRepository.BeginTransaction())
            {
                try
                {
                    var bird = _mapper.Map<Bird>(birdAddFromEggRequest);
                    if (bird == null)
                    {
                        return -1;
                    }

                    var egg = await _eggRepository.GetEggDetailsAsync(birdAddFromEggRequest.EggId);
                    if (egg == null || egg.Clutch == null)
                    {
                        return -1;
                    }

                    var breeding = egg.Clutch.Breeding;
                    if(breeding == null || breeding.FatherBird == null)
                    {
                        return -1;
                    }

                    var cage = await _cageRepository.GetByIdAsync(birdAddFromEggRequest.CageId);
                    if (cage != null && cage.Area != null)//cage never null
                    {
                        bird.FarmId = cage.Area.FarmId;
                    }

                    bird.BirdSpeciesId = breeding.FatherBird.BirdSpeciesId;
                    bird.FatherBirdId = breeding.FatherBirdId;
                    bird.MotherBirdId = breeding.MotherBirdId;
                    bird.HatchedDate = egg.HatchedDate;
                    await _birdRepository.AddAsync(bird);
                    _birdRepository.SaveChanges();

                    if (birdAddFromEggRequest.MutationRequests != null && birdAddFromEggRequest.MutationRequests.Any())
                    {
                        await AddMutation(birdAddFromEggRequest.MutationRequests, bird.BirdId);
                    }

                    EggBird eggBird = new EggBird()
                    {
                        EggId = birdAddFromEggRequest.EggId,
                        BirdId = bird.BirdId
                    };
                    await _eggBirdRepository.AddAsync(eggBird);
                    _eggBirdRepository.SaveChanges();

                    transaction.Commit();
                    return bird.BirdId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                    return -1;
                }
            }
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

        public async Task<IEnumerable<BirdResponse>> GetBirdsByStaffId(object staffId)
        {
            var birds = await _birdRepository.GetBirdsByStaffId(staffId);
            return birds.Select(b => _mapper.Map<BirdResponse>(b));
        }
        public async Task<int> GetTotalBirdCountByStaffId(object staffId)
        {
            var birds = await _birdRepository.GetBirdsByStaffId(staffId);
            return birds.Count();
        }

        public async Task<BirdDetailResponse?> GetBirdByIdAsync(object birdId)
        {
            var bird = await _birdRepository.GetByIdAsync(birdId);
            if (bird == null)
            {
                return null;
            }

            var birdResponse = _mapper.Map<BirdDetailResponse>(bird);
            if(bird.BirdMutations.Any())
            {
                List<IndividualMutation> mutations = new List<IndividualMutation>();
                foreach (var item in bird.BirdMutations)
                {
                    var individualMutation = _mapper.Map<IndividualMutation>(item.Mutation);
                    mutations.Add(individualMutation);
                }

                birdResponse.IndividualMutations = mutations;
            }
            
            return birdResponse;
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
            using (var transaction = _birdRepository.BeginTransaction())
            {
                try
                {
                    var bird = await _birdRepository.GetByIdAsync(birdUpdateRequest.BirdId);
                    if (bird == null)
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

                    _birdRepository.SaveChanges();

                    if (birdUpdateRequest.MutationRequests != null)
                    {
                        await UpdateBirdMutations(birdUpdateRequest.MutationRequests, bird.BirdId);
                    }

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

        public async Task<int> GetTotalBirdByAccountIdAsync()
        {
            var birds = await _birdRepository.GetAllAsync();
            return birds.Count();
        }

        private async Task<bool> UpdateBirdMutations(List<MutationRequest> mutationRequests, int birdId)
        {
            var bird = await _birdRepository.GetByIdAsync(birdId);
            if (bird == null)
            {
                return false;
            }

            var birdMutations = await _birdMutationRepository.GetByBirdId(birdId);
            if(birdMutations.Any())
            {
                _birdMutationRepository.Delete(birdMutations);
            }

            if(mutationRequests.Any())
            {
                await AddMutation(mutationRequests, birdId);
            }

            return true;
        }

        private async Task AddMutation (List<MutationRequest> mutationRequests, int birdId)
        {
            foreach (var item in mutationRequests)
            {
                var birdMutation = new BirdMutation()
                {
                    BirdId = birdId,
                    MutationId = item.MutationId
                };
                await _birdMutationRepository.AddAsync(birdMutation);
            }

            _birdMutationRepository.SaveChanges();
        }
    }
}
