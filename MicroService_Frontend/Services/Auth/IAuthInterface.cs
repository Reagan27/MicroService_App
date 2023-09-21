
using Frontend.Models;
using MicroService_Frontend.Models.Auth;
using MicroService_Frontend.Models.Posts;

namespace MicroService_Frontend.Services.Auth
{
    public interface IAuthInterface
    {
        Task<ResponseDto> Register(RegisterRequestDto registerRequestDto);
        Task<LoginResponseDto> Login (LoginRequestDto loginRequestDto);
    }
}