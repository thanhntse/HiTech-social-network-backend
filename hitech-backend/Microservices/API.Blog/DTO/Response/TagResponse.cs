using System.ComponentModel.DataAnnotations.Schema;

namespace API.Blog.DTO.Response
{
    public class TagResponse
    {
        public int TagId { get; set; }
        public string TagName { get; set; } = string.Empty;
    }
}
