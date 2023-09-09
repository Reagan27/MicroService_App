using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore; 
using Auth.Models;

namespace Auth.Data{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}
        public DbSet<ApplicationUser> ApplicationUsers {get; set; }
    }
    
}