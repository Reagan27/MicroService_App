using Frontend.Models;
using MicroService_Frontend.Models.Posts;

public interface IPostInterface
{
    Task<ResponseDto> AddPostAsync(PostRequestDto newPost);
    Task<ResponseDto> DeletePostAsync(Guid id);
    Task<List<PostDto>> GetAllPostsAsync();

    Task<PostDto> GetPostById(Guid postId);
    Task<IEnumerable<PostDto>> GetUserPosts(Guid userId);
    Task<ResponseDto> UpdatePostAsync(Guid id, PostRequestDto updatePost);
}
