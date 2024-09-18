using HiTech.Service.BlogAPI.DTOs.Request;
using HiTech.Service.BlogAPI.DTOs.Response;
using HiTech.Service.BlogAPI.Entities;
using AutoMapper;
namespace HiTech.Service.BlogAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BlogRequest, Entities.Blog>();
            CreateMap<Entities.Blog, BlogResponse>();

            CreateMap<CommentRequest, Comment>();
            CreateMap<Comment, CommentResponse>();

            CreateMap<Tag, TagResponse>();
        }
    }
}
