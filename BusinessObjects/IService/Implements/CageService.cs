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
    public class CageService : ICageService
    {
        private readonly ICageRepository _cageRepository;
        private readonly IMapper _mapper;
        public CageService(ICageRepository cageRepository, IMapper mapper)
        {
            _cageRepository = cageRepository;
            _mapper = mapper;
        }

        public async Task CreateCageAsync(Cage cage)
        {
            await _cageRepository.AddAsync(cage);
            _cageRepository.SaveChanges();
        }

        public void DeleteCage(Cage cage)
        {
            throw new NotImplementedException();
        }

        public void DeleteCageById(object cageId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CageResponse>> GetAllCagesAsync()
        {
            var cages = await _cageRepository.GetAllAsync();
            return cages.Select(c => _mapper.Map<CageResponse>(c));
        }

        public async Task<CageDetailResponse?> GetCageByIdAsync(object cageId)
        {
            var cage = await _cageRepository.GetByIdAsync(cageId);
            return _mapper.Map<CageDetailResponse>(cage);
        }

        public void UpdateCage(Cage cage)
        {
            throw new NotImplementedException();
        }
    }
}
