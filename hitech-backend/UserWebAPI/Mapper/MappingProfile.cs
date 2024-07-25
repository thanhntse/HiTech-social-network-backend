using AutoMapper;
using UserWebAPI.DTO.Request;
using UserWebAPI.DTO.Response;
using UserWebAPI.Entities;

namespace UserWebAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRequest, User>();
            CreateMap<User, UserResponse>();
        }
    }
}
