namespace Frontend.Models.Dtos
{
    public class CommentResponseDto
    {
        public object? Result { get; set; }

        public bool IsSuccess { get; set; } = true;

        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; }
    }
}