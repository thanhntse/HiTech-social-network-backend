﻿using AutoMapper;
using HiTech.Service.AuthAPI.DTOs.Message;
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
            CreateMap<AccountUpdationRequest, AccountInfo>();
            CreateMap<Account, AccountResponse>();

            CreateMap<Account, UserMessage>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.AccountId));
        }
    }
}
