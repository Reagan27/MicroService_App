using JituPost.Data;
using JituPost.Models;
using JituPost.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace JituPost.Services
{
    public class PostService : IPostServices
    {
        private readonly AppDbContext _context;

        public PostService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ThePost>> GetPostsAsync()
        {
            // Retrieve all posts from the database
            return await _context.Posts.ToListAsync();
        }

        public async Task<ThePost> GetPostByIdAsync(Guid id)
        {
            // Retrieve a post by its ID from the database
            return await _context.Posts.FirstOrDefaultAsync(post => post.Id == id);
        }

        public async Task<string> CreatePostAsync(ThePost post)
        {
              
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
                return "Post created successfully.";
         
        }

        public async Task<string> UpdatePostAsync(ThePost post)
        {
                _context.Entry(post).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return "Post updated successfully.";
          
        }

        public async Task<string> DeletePostAsync(ThePost post)
        {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return "Post deleted successfully.";
          
        }
    }
}
