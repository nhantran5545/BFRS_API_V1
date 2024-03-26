using AutoMapper;
using BusinessObjects.ResponseModels;
using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService.Implements
{
    public class BreedingCheckListDetailService : IBreedingCheckListDetailService
    {
        private readonly IBreedingCheckListDetailRepository _breedingCheckListDetailRepository;
        private readonly IMapper _mapper;

        public BreedingCheckListDetailService(IBreedingCheckListDetailRepository breedingCheckListDetailRepository, IMapper mapper)
        {
            _breedingCheckListDetailRepository = breedingCheckListDetailRepository ?? throw new ArgumentNullException(nameof(breedingCheckListDetailRepository));
            _mapper = mapper;
        }

    }
}
