using HiTech.Service.BlogAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiTech.Service.BlogAPI.DTOs.Request
{
    public class CommentRequest
    {
        public string Content { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public int BlogId { get; set; }
    }
}
