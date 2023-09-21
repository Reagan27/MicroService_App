using System.ComponentModel.DataAnnotations;

namespace MicroService_Frontend.Models.Posts
{
    public class PostRequestDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Body { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}