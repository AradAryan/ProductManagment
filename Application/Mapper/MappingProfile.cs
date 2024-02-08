using Application.Dtos;
using AutoMapper;
using Domain.Identity;

namespace Application.Mapper
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
