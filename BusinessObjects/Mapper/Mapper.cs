using AutoMapper;
using BusinessObjects.ResponseModels;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Mapper
{
    public class Mapper : Profile
    {
        public Mapper() 
        {
            #region Mapper_Response
            CreateMap<Bird, BirdReponse>()
                .ForMember(dest => dest.BirdSpeciesName,
                            opt => opt.MapFrom(src => src.BirdSpecies != null ? src.BirdSpecies.BirdSpeciesName : string.Empty))
                .ReverseMap();
            CreateMap<Bird, BirdDetailResponse>()
                .ForMember(dest => dest.BirdSpeciesName,
                            opt => opt.MapFrom(src => src.BirdSpecies != null ? src.BirdSpecies.BirdSpeciesName : string.Empty))
                /*.ForMember(dest => dest.MutationName,
                            opt => opt.MapFrom(src => src.Mutation != null ? src.Mutation.MutationName : string.Empty))*/
                .ReverseMap();
            CreateMap<Cage, CageResponse>()
                .ForMember(dest => dest.AreaName,
                            opt => opt.MapFrom(src => src.Area != null ? src.Area.AreaName : string.Empty))
                .ReverseMap();
            CreateMap<Cage, CageDetailResponse>()
                .ForMember(dest => dest.AreaName,
                            opt => opt.MapFrom(src => src.Area != null ? src.Area.AreaName : string.Empty))
                .ReverseMap();
            #endregion
        }
    }
}
