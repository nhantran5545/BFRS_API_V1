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
            CreateMap<Account, AccountDetailResponse>()
                  .ReverseMap(); 
            CreateMap<Account, ProfileResponse>()
                  .ReverseMap();
            CreateMap<Account, AccountResponse>()
                 .ReverseMap();
            CreateMap<Area, AreaResponse>()
                .ForMember(dest => dest.FarmName,
                            opt => opt.MapFrom(src => src.Farm != null ? src.Farm.FarmName : string.Empty))
                .ReverseMap();
            CreateMap<Bird, BirdResponse>()
                .ForMember(dest => dest.BirdSpeciesName,
                            opt => opt.MapFrom(src => src.BirdSpecies != null ? src.BirdSpecies.BirdSpeciesName : string.Empty))
                .ForMember(dest => dest.FarmName,
                            opt => opt.MapFrom(src => src.Farm != null ? src.Farm.FarmName : string.Empty))
                .ReverseMap();
            CreateMap<Bird, BirdDetailResponse>()
                .ForMember(dest => dest.BirdSpeciesName,
                            opt => opt.MapFrom(src => src.BirdSpecies != null ? src.BirdSpecies.BirdSpeciesName : string.Empty))
                .ReverseMap();
            CreateMap<Bird, BirdPedi>()
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
                .ForMember(dest => dest.FatherBandNumber,
                            opt => opt.MapFrom(src => src.FatherBird != null ? src.FatherBird.BandNumber : null))
                .ForMember(dest => dest.FatherImage,
                            opt => opt.MapFrom(src => src.FatherBird != null ? src.FatherBird.Image : null))
                .ForMember(dest => dest.MotherBandNumber,
                            opt => opt.MapFrom(src => src.MotherBird != null ? src.MotherBird.BandNumber : null))
                .ForMember(dest => dest.MotherImage,
                            opt => opt.MapFrom(src => src.MotherBird != null ? src.MotherBird.Image: null))
                .ForMember(dest => dest.SpeciesId,
                            opt => opt.MapFrom(src => src.MotherBird != null ? src.MotherBird.BirdSpeciesId : null))
                .ForMember(dest => dest.NumOfClutches,
                            opt => opt.MapFrom(src => src.Clutches.Count()))
                .ReverseMap();
            CreateMap<Breeding, BreedingDetailResponse>()
                .ForMember(dest => dest.FatherBandNumber,
                            opt => opt.MapFrom(src => src.FatherBird != null ? src.FatherBird.BandNumber : null))
                .ForMember(dest => dest.FatherImage,
                            opt => opt.MapFrom(src => src.FatherBird != null ? src.FatherBird.Image : null))
                .ForMember(dest => dest.MotherBandNumber,
                            opt => opt.MapFrom(src => src.MotherBird != null ? src.MotherBird.BandNumber : null))
                .ForMember(dest => dest.MotherImage,
                            opt => opt.MapFrom(src => src.MotherBird != null ? src.MotherBird.Image : null))
                .ForMember(dest => dest.SpeciesId,
                            opt => opt.MapFrom(src => src.MotherBird != null ? src.MotherBird.BirdSpeciesId : null))
                .ReverseMap();
            CreateMap<BreedingCheckList, BreedingCheckListResponse>()
                .ReverseMap();
            CreateMap<BreedingCheckListDetail, BreedingCheckListDetailResponse>()
                .ReverseMap();
            CreateMap<Cage, CageResponse>()
                .ForMember(dest => dest.AreaName,
                            opt => opt.MapFrom(src => src.Area != null ? src.Area.AreaName : string.Empty))
                .ForMember(dest => dest.FirstName,
                            opt => opt.MapFrom(src => src.Account != null ? src.Account.FirstName : string.Empty))
                .ForMember(dest => dest.LastName,
                            opt => opt.MapFrom(src => src.Account != null ? src.Account.LastName : string.Empty))
                .ForMember(dest => dest.NumOfBird,
                            opt => opt.MapFrom(src => src.Birds.Count()))
                .ReverseMap();
            CreateMap<Cage, CageDetailResponse>()
                .ForMember(dest => dest.AreaName,
                            opt => opt.MapFrom(src => src.Area != null ? src.Area.AreaName : string.Empty))
                .ForMember(dest => dest.FirstName,
                            opt => opt.MapFrom(src => src.Account != null ? src.Account.FirstName : string.Empty))
                .ForMember(dest => dest.LastName,
                            opt => opt.MapFrom(src => src.Account != null ? src.Account.LastName : string.Empty))
                                .ForMember(dest => dest.BirdQuantity,
                            opt => opt.MapFrom(src => src.Birds.Count()))
                .ReverseMap();
            CreateMap<Clutch, ClutchResponse>()
                .ForMember(dest => dest.CreatedByFirstName,
                            opt => opt.MapFrom(src => src.CreatedByNavigation != null ? src.CreatedByNavigation.FirstName : string.Empty))
                .ForMember(dest => dest.CreatedByLastName,
                            opt => opt.MapFrom(src => src.CreatedByNavigation != null ? src.CreatedByNavigation.LastName : string.Empty))
                .ForMember(dest => dest.NumOfEggs,
                            opt => opt.MapFrom(src => src.Eggs.Count()))
                .ReverseMap();
            CreateMap<CheckList, CheckListResponse>()
                .ReverseMap();
            CreateMap<CheckListDetail, CheckListDetailResponse>()
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
                .ForMember(dest => dest.BirdId,
                            opt => opt.MapFrom(src => src.EggBirds.Any() ? src.EggBirds.First().BirdId : 0))
                .ReverseMap();
            CreateMap<Mutation, IndividualMutation>()
                .ReverseMap();
            CreateMap<CheckList, CheckListResponse>()
                .ReverseMap();
            CreateMap<Farm, FarmResponse>()

                .ReverseMap();
            CreateMap<Mutation, IndividualMutation>()
                .ReverseMap();
            CreateMap<Issue, IssueResponse>()
                .ForMember(dest => dest.CreatedByFirstName,
                            opt => opt.MapFrom(src => src.CreatedByNavigation != null ? src.CreatedByNavigation.FirstName : string.Empty))
                .ForMember(dest => dest.CreatedByLastName,
                            opt => opt.MapFrom(src => src.CreatedByNavigation != null ? src.CreatedByNavigation.LastName : string.Empty))
                .ForMember(dest => dest.UpdatedByFirstName,
                            opt => opt.MapFrom(src => src.UpdatedByNavigation != null ? src.UpdatedByNavigation.FirstName : string.Empty))
                .ForMember(dest => dest.UpdatedByLastName,
                            opt => opt.MapFrom(src => src.UpdatedByNavigation != null ? src.UpdatedByNavigation.LastName : string.Empty))
                .ForMember(dest => dest.IssueTypeName,
                            opt => opt.MapFrom(src => src.IssueType != null ? src.IssueType.IssueName : string.Empty))
                .ReverseMap();
            CreateMap<CheckListDetail, CheckListDetailResponse>()
                .ReverseMap(); 
            CreateMap<IssueType, IssueTypeResponse>()
                .ReverseMap();

            #endregion

            #region Mapper_Request
            CreateMap<AccountSignUpRequest, Account>()
                .ReverseMap();
            CreateMap<AreaAddRequest, Area>()
                .ReverseMap();
            CreateMap<BirdAddRequest, Bird>()
                .ReverseMap();
            CreateMap<BirdSpeciesRequest, BirdSpecy>()
                .ReverseMap();
            CreateMap<BirdAddFromEggRequest, Bird>()
                .ReverseMap();
            CreateMap<BreedingAddRequest, Breeding>()
                .ReverseMap();
            CreateMap<BreedingCheckListAddRequest, BreedingCheckList>()
                .ReverseMap();
            CreateMap<BreedingCheckListAddRequestDetail, BreedingCheckListDetail>()
                .ReverseMap();
            CreateMap<ClutchAddRequest, Clutch>()
                .ReverseMap();
            CreateMap<ClutchCheckListAddRequest, BreedingCheckList>()
                .ReverseMap();
            CreateMap<CageAddRequest, Cage>()
                .ReverseMap();
            CreateMap<ClutchCloseRequest, Clutch>()
                .ReverseMap();
            CreateMap<EggAddRequest, Egg>()
                .ReverseMap();
            CreateMap<MutationRequest, Mutation>()
                .ReverseMap(); 
            CreateMap<FarmRequest, Farm>()
                .ReverseMap();
            CreateMap<AccountLoginRequest, Account>()
                .ReverseMap();            
            CreateMap<IssueAddRequest, Issue>()
                .ReverseMap();
            CreateMap<IssueTypeRequest, IssueType>()
                .ReverseMap();
            #endregion
        }
    }
}
