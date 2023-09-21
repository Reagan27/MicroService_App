
using Frontend.Models;
using MicroService_Frontend.Models.Auth;

namespace MicroService_Frontend.Services.Auth{
    public interface IAuthInterface
    {
        Task<ResponseDto> Register(RegisterRequestDto registerRequestDto);
        Task<LoginResponseDto> Login (LoginRequestDto loginRequestDto);
    }
}