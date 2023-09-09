using Microsoft.AspNetCore.Identity;

namespace Auth.Models{
    public class ApplicationUser:IdentityUser{
         public string Name {get; set;} = string.Empty;
    }
   
}