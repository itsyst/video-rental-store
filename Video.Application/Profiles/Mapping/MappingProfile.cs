using AutoMapper;
using Video.Application.Profiles.Dtos;
using Video.Domain.Entities;
using Video.Domain.ValueObject;

namespace Video.Application.Profiles.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<Movie, MovieDto>().ReverseMap();
        CreateMap<Genre, GenreDto>().ReverseMap();
        CreateMap<MembershipType, MembershipTypeDto>().ReverseMap();

        // Cast object of type 'CustomerDto' to type 'Customer'.
        CreateMap<MinAgeDto, MinAge>().ReverseMap();  
    }
}
