namespace Frontend.Models.Comments
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string CommentText { get; set; } = "";
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}