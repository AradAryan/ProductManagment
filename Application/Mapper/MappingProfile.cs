using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.IdentityEntities;

namespace Application.Mapper
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            //CreateMap<List<Product>, List<ProductDto>>();
        }
    }
}
