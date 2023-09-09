using JituPost.Models;
namespace JituPost.Service.IService{
    public interface IPostServices
{
    Task<IEnumerable<ThePost>> GetPostsAsync();
    Task<ThePost> GetPostByIdAsync(Guid id);
    Task<string> CreatePostAsync(ThePost post);
    Task<string> UpdatePostAsync(ThePost post);
    Task<string> DeletePostAsync(ThePost post);
}
}