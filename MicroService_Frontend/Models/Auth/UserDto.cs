namespace MicroService_Frontend.Models.Auth{
    public class UserDto{
      
        public Guid Id {get; set; } 
 
        public string Name {get; set; } = string.Empty;


        public string Email {get; set; } = string.Empty;
      
        public string PhoneNumber {get; set; } = string.Empty;

    }
}