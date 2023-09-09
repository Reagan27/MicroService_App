namespace JituComments.Models{
    public class Comments
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public int PostId { get; set; } // Foreign key to identify the post to which the comment belongs
    public int UserId { get; set; } // Foreign key to identify the user who posted the comment
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

}