using RCE_Auth.Register.DTOs;

namespace RCE_Auth.Register.Services;

public interface IRegisterService
{
    Task<RegisterResponseDTO> RegisterUserAsync(RegisterRequestDTO dto);
}
