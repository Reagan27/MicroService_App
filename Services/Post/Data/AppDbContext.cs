using Microsoft.EntityFrameworkCore;
using JituPost.Models;

namespace JituPost.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<ThePost> Posts { get; set; }
    }
}