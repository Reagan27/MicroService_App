using Auth.Models;
namespace Auth.Services.IService{
    public interface IJWtTokenGenerator{
        string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}