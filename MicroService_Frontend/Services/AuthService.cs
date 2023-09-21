using Frontend.Models;
using MicroService_Frontend.Models.Auth;
using MicroService_Frontend.Models.Posts;
using Newtonsoft.Json;
using System.Text;

namespace MicroService_Frontend.Services.Auth
{
    public class AuthService : IAuthInterface
    {
        private readonly HttpClient _httpClient;
        private readonly string BASEURL = "http://localhost:5172";
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
           var request=JsonConvert.SerializeObject(loginRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api

            var response = await _httpClient.PostAsync($"{BASEURL}/api/User/login", bodyContent);
            var content =await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                return JsonConvert.DeserializeObject<LoginResponseDto>(results.Result.ToString());

            }
           return new LoginResponseDto();
        }

        public async Task<ResponseDto> Register(RegisterRequestDto registerRequestDto)
        {
            var request = JsonConvert.SerializeObject(registerRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api

            var response = await _httpClient.PostAsync($"{BASEURL}/api/User/register", bodyContent);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
            
                return results;

            }
            return new ResponseDto();
        }
    }

   
}