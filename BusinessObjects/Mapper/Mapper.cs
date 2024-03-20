using AutoMapper;
using BusinessObjects.RequestModels;
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
            CreateMap<Bird, BirdResponse>()
                .ForMember(dest => dest.BirdSpeciesName,
                            opt => opt.MapFrom(src => src.BirdSpecies != null ? src.BirdSpecies.BirdSpeciesName : string.Empty))
                .ReverseMap();
            CreateMap<Bird, BirdDetailResponse>()
                .ForMember(dest => dest.BirdSpeciesName,
                            opt => opt.MapFrom(src => src.BirdSpecies != null ? src.BirdSpecies.BirdSpeciesName : string.Empty))
                .ReverseMap();
            CreateMap<BirdSpecy, BirdSpeciesResponse>()
                .ForMember(dest => dest.BirdTypeName,
                            opt => opt.MapFrom(src => src.BirdType != null ? src.BirdType.BirdTypeName : string.Empty))
                .ReverseMap();
            CreateMap<BirdSpecy, BirdSpeciesDetailResponse>()
                .ForMember(dest => dest.BirdTypeName,
                            opt => opt.MapFrom(src => src.BirdType != null ? src.BirdType.BirdTypeName : string.Empty))
                .ReverseMap();
            CreateMap<Breeding, BreedingResponse>()
                .ReverseMap();
            CreateMap<Breeding, BreedingDetailResponse>()
                .ReverseMap();
            CreateMap<Cage, CageResponse>()
                .ForMember(dest => dest.AreaName,
                            opt => opt.MapFrom(src => src.Area != null ? src.Area.AreaName : string.Empty))
                .ForMember(dest => dest.FirstName,
                            opt => opt.MapFrom(src => src.Account != null ? src.Account.FirstName : string.Empty))
                .ForMember(dest => dest.LastName,
                            opt => opt.MapFrom(src => src.Account != null ? src.Account.LastName : string.Empty))
                .ReverseMap();
            CreateMap<Cage, CageDetailResponse>()
                .ForMember(dest => dest.AreaName,
                            opt => opt.MapFrom(src => src.Area != null ? src.Area.AreaName : string.Empty))
                .ForMember(dest => dest.FirstName,
                            opt => opt.MapFrom(src => src.Account != null ? src.Account.FirstName : string.Empty))
                .ForMember(dest => dest.LastName,
                            opt => opt.MapFrom(src => src.Account != null ? src.Account.LastName : string.Empty))
                .ReverseMap();
            CreateMap<Clutch, ClutchResponse>()
                .ReverseMap();
            CreateMap<Clutch, ClutchDetailResponse>()
                .ForMember(dest => dest.CreatedByFirstName,
                            opt => opt.MapFrom(src => src.CreatedByNavigation != null ? src.CreatedByNavigation.FirstName : string.Empty))
                .ForMember(dest => dest.CreatedByLastName,
                            opt => opt.MapFrom(src => src.CreatedByNavigation != null ? src.CreatedByNavigation.LastName : string.Empty))
                .ForMember(dest => dest.UpdatedByFirstName,
                            opt => opt.MapFrom(src => src.UpdatedByNavigation != null ? src.UpdatedByNavigation.FirstName : string.Empty))
                .ForMember(dest => dest.UpdatedByLastName,
                            opt => opt.MapFrom(src => src.UpdatedByNavigation != null ? src.UpdatedByNavigation.LastName : string.Empty))
                .ReverseMap();
            CreateMap<Egg, EggResponse>()
                .ReverseMap();
            CreateMap<Mutation, IndividualMutation>()
                .ReverseMap();
            #endregion

            #region Mapper_Request
            CreateMap<BirdAddRequest, Bird>()
                .ReverseMap();
            CreateMap<BreedingAddRequest, Breeding>()
                .ReverseMap();
            CreateMap<ClutchAddRequest, Clutch>()
                .ReverseMap();
            #endregion
        }
    }
}
