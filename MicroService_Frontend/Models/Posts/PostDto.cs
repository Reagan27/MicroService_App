using MicroService_Frontend.Models;

namespace MicroService_Frontend.Models.Posts
{
    public class PostDto
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public IEnumerable<CommentsDto>? Comments { get; set; }

        public class CommentsDto
        {
        }
    }
}
