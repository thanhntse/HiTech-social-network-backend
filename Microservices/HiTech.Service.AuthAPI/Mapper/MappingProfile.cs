using AutoMapper;
using HiTech.Service.AuthAPI.DTOs.Request;
using HiTech.Service.AuthAPI.DTOs.Response;
using HiTech.Service.AuthAPI.Entities;

namespace HiTech.Service.AuthAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountCreationRequest, Account>();
            CreateMap<AccountUpdationRequest, Account>();
            CreateMap<Account, AccountResponse>();
        }
    }
}
