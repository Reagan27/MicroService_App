using Frontend.Models;
using Frontend.Models.Comments;

namespace MicroService_Frontend.Services.Interfaces
{
    public interface ICommentInterface
    {
        
        Task<List<Comment>> GetAllComments();
        
        Task<ResponseDto> CreateComment(CommentRequestDto commentRequestDto);

        
    }
}