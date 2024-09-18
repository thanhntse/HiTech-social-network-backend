using System.ComponentModel.DataAnnotations.Schema;

namespace HiTech.Service.BlogAPI.DTOs.Request
{
    public class BlogRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public List<int> TagIds { get; set; } = null!;
    }
}
