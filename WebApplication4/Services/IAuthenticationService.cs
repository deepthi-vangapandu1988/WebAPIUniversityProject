using WebApplication4.Dto;

namespace WebApplication4.Services
{
    public interface IAuthenticationService
    {
        Task<LoginResponseDTO> RegisterAsync(RegisterDTO model);
        Task<LoginResponseDTO> LoginAsync(LoginDTO model);
    }
}
