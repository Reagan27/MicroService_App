namespace MicroService_Frontend.Models.Posts
{
    public class PostResponseDto
    {
        public object? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = true;
    }
}