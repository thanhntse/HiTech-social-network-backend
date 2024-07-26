using API.Blog.DTO.Request;
using API.Blog.DTO.Response;
using API.Blog.Entities;
using AutoMapper;
namespace API.Blog.Mapper
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
