using JituPost.Models.Dtos;

namespace JituPost.Services.IService
{
    public interface ICommentInterface
    {
       Task <IEnumerable<CommentsDto>> GetCommentsAsync (Guid id);
    }
}