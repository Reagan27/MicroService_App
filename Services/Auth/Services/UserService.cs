using AutoMapper;
using Auth.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Auth.Services.IService;
using Auth.Data;
using Auth.Models;


namespace Auth.Services{
    public class UserService: IUserInterface{
   private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IJWtTokenGenerator _jwtGenerator;
        public UserService(ApplicationDbContext database , UserManager<ApplicationUser> userManager, IJWtTokenGenerator tokenGenerator, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = database;
            _mapper = mapper;
            _jwtGenerator = tokenGenerator;
        }

        public async Task<bool> AssignUserRole(string email, string Rolename)
        {
             //Get user by email
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if(user != null) 
            {

               
                //check if role exist
                if(!_roleManager.RoleExistsAsync(Rolename).GetAwaiter().GetResult())
                {   
                    //first create it
                     _roleManager.CreateAsync(new IdentityRole(Rolename)).GetAwaiter().GetResult();
                }

                await _userManager.AddToRoleAsync(user, Rolename);

                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
          
           var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u=>u.UserName.ToLower()==loginRequestDto.UserName.ToLower());
           
           var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            //checks if user is null or password is wrong
            if(!isValid || user == null)
            {
                new LoginResponseDto();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtGenerator.GenerateToken(user, roles);
            
            var loggedUser = new LoginResponseDto()
            {
                User = _mapper.Map<UserDto>(user),
                Token = token
            };

            return loggedUser;
        }


        public async Task<string> RegisterUser(RegisterRequestDto registerRequestDto){
            var user = _mapper.Map<ApplicationUser>(registerRequestDto);

            try
            {   

                //user is created 
                var result = await _userManager.CreateAsync(user, registerRequestDto.Password);

                if(result.Succeeded)
                {   
                    //if success return empty string
                  

                    //var get User 
                    //var existingUser = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == registerRequestDto.Email.ToLower());
                    return "";
                }
                else
                {

                    //identity Error if any
                    return result.Errors.FirstOrDefault().Description;
                }
            }catch (Exception ex) {
                return ex.Message;
            }
        }
    
    }
}