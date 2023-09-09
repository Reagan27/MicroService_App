using Microsoft.EntityFrameworkCore;
using EmailService.Models;

namespace EmailService.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<EmailLoggers> EmailLoggers { get; set; }                   
    }
}