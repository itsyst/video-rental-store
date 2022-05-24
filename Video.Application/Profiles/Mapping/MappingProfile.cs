using AutoMapper;
using Video.Application.Profiles.Dtos;
using Video.Domain.Entities;
using Video.Domain.ValueObject;

namespace Video.Application.Profiles.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Genre, GenreDto>().ReverseMap();
        CreateMap<Movie, MovieDto>().ReverseMap();
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<MembershipType, MembershipTypeDto>().ReverseMap();

        //// Domain to Dto
        ////CreateMap<Movie, MovieDto>()
        ////    .ForMember(m => m.Id, opt => opt.Ignore()); 
        //CreateMap<Customer, CustomerDto>()
        //    .ForMember(m => m.Id, opt => opt.Ignore());

        //// Dto to Domain
        ////CreateMap<MovieDto, Movie>()
        ////    .ForMember(m => m.Id, opt => opt.Ignore());
        //CreateMap<CustomerDto, Customer>()
        //    .ForMember(m => m.Id, opt => opt.Ignore());

        // Cast object of type 'CustomerDto' to type 'Customer'.
        CreateMap<MinAgeDto, MinAge>().ReverseMap();
    }
}
