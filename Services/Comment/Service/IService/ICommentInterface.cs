using JituComments.Models;

namespace JituComments.Services.IService{
    public interface ICommentService
{
    Task<IEnumerable<Comments>> GetAllCommentsAsync();
    Task<Comments> GetCommentByIdAsync(Guid id);
    Task<string> CreateCommentAsync(Comments comment);
    Task<string> UpdateCommentAsync(Comments comment);
    Task<string> DeleteCommentAsync(Comments comment);
    Task <IEnumerable<Comments>> GetCommentByPostAsync (Guid id);
}
}