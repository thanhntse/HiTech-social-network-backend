using AutoMapper;
using HiTech.Service.PostsAPI.DTOs.Request;
using HiTech.Service.PostsAPI.DTOs.Response;
using HiTech.Service.PostsAPI.Entities;

namespace HiTech.Service.PostsAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CommentRequest, Comment>();
            CreateMap<Comment, CommentReponse>();

            CreateMap<PostRequest, Post>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src =>
                    src.Images.Select((imageUrl, index) => new Image
                    {
                        ImageUrl = imageUrl,
                        ImageNo = index + 1
                    }).ToList()));
            CreateMap<Post, PostResponse>();

            CreateMap<Image, ImageResponse>();
        }
    }
}
