using AutoMapper;
using HiTech.Service.NotificationAPI.DTOs.Request;
using HiTech.Service.NotificationAPI.DTOs.Response;
using HiTech.Service.NotificationAPI.Entities;

namespace HiTech.Service.NotificationAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NotificationRequest, Notification>();
            CreateMap<Notification, NotificationResponse>();
        }
    }
}
