using API.User.DTO.Request;
using API.User.DTO.Response;
using API.User.Entities;
using AutoMapper;

namespace API.User.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRequest, Entities.User>();
            CreateMap<Entities.User, UserResponse>();
        }
    }
}
