using AutoMapper;
using BlogWebAPI.DTO.Request;
using BlogWebAPI.DTO.Response;
using BlogWebAPI.Entities;
namespace UserWebAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BlogRequest, Blog>();
            CreateMap<Blog, BlogResponse>();

            CreateMap<CommentRequest, Comment>();
            CreateMap<Comment, CommentResponse>();
        }
    }
}
