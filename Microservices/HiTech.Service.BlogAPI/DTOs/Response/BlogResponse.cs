using HiTech.Service.BlogAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiTech.Service.BlogAPI.DTOs.Response
{
    public class BlogResponse
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime PublishedAt { get; set; }
        public int Like { get; set; } = 0;
        public int CommentsCount { get; set; } = 0;
        public int AuthorId { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<TagResponse> Tags { get; set; } = new List<TagResponse>();
    }
}
