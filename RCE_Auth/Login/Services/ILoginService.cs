using RCE_Auth.Login.DTOs;

namespace RCE_Auth.Login.Services
{
    public interface ILoginService
    {
        Task<LoginResponseDTO> AuthenticateAsync(LoginRequestDTO dto);
    }
}
