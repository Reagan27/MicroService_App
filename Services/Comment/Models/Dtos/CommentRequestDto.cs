namespace JituComments.Models.Dtos{
    public class CommentRequestDto
{
    // public Guid Id { get; set; }
    public string Content { get; set; } = "";
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
}
}
