namespace JituComments.Models.Dtos{
    public class CommentRequestDto
{
    public Guid Id { get; set; }
    public string Content { get; set; } = "";
    public int PostId { get; set; }
    public int UserId { get; set; }
}
}
