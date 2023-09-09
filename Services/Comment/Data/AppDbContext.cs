using Microsoft.EntityFrameworkCore;
using JituComments.Models;

namespace JituComments.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Comments> Comments { get; set; }
    }
}