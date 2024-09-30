using AutoMapper;
using HiTech.Service.FriendAPI.DTOs.Response;
using HiTech.Service.FriendAPI.Entities;

namespace HiTech.Service.FriendAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FriendRequest, FriendRequestResponse>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Receiver ?? src.Sender));
            CreateMap<Friendship, FriendshipResponse>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UserReceived ?? src.UserSent));

            CreateMap<User, User>();
            CreateMap<User, UserResponse>();
        }
    }
}
