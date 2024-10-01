using AutoMapper;
using HiTech.Service.GroupAPI.DTOs.Request;
using HiTech.Service.GroupAPI.DTOs.Response;
using HiTech.Service.GroupAPI.Entities;

namespace HiTech.Service.GroupAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GroupRequest, Group>();
            CreateMap<Group, GroupResponse>();

            CreateMap<JoinRequest, JoinRequestResponse>();

            CreateMap<User, User>();
            CreateMap<User, UserResponse>();
        }
    }
}
