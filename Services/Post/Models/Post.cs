using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JituPost.Models.Dtos;

namespace JituPost.Models{
public class ThePost
{
    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid UserId { get; set; } // Foreign key to identify the user who created the post
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [NotMapped]
    public IEnumerable<CommentsDto> Comments { get; set; } = new List<CommentsDto>();


  
}
}
