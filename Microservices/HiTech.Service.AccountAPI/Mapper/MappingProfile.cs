using HiTech.Service.AccountAPI.DTOs.Request;
using HiTech.Service.AccountAPI.DTOs.Response;
using HiTech.Service.AccountAPI.Entities;
using AutoMapper;

namespace HiTech.Service.AccountAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountRequest, Account>();
            CreateMap<Account, AccountResponse>();
        }
    }
}
