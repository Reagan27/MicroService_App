using JituComments.Data;
using JituComments.Models;
using JituComments.Services.IService;
using Microsoft.EntityFrameworkCore;


namespace JituComments.Services
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;

        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comments>> GetAllCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comments> GetCommentByIdAsync(Guid id)
        {
            return await _context.Comments.FirstOrDefaultAsync(comment => comment.Id == id);
        }

        public async Task<string> CreateCommentAsync(Comments comment)
        {
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return "Comment created successfully.";
        }

        public async Task<string> UpdateCommentAsync(Comments comment)
        {
                _context.Entry(comment).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return "Comment updated successfully.";
           
        }

        public async Task<string> DeleteCommentAsync(Comments comment)
        {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
                return "Comment deleted successfully.";
            }

        public async Task<IEnumerable<Comments>> GetCommentByPostAsync(Guid id)
        {
             return await _context.Comments.Where(comment => comment.PostId == id).ToListAsync();
        }
    }
    }

