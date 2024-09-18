using System.ComponentModel.DataAnnotations.Schema;

namespace HiTech.Service.BlogAPI.DTOs.Response
{
    public class TagResponse
    {
        public int TagId { get; set; }
        public string TagName { get; set; } = string.Empty;
    }
}
