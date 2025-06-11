using Microsoft.AspNetCore.Identity;
using RCE_Auth.Register.DTOs;
using RCE_Auth.UsersRoles.Entities;
using RCE_Auth.UsersRoles.Repositories;

namespace RCE_Auth.Register.Services;

public class RegisterService : IRegisterService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public RegisterService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterResponseDTO> RegisterUserAsync(RegisterRequestDTO dto)
    {
        if (_userRepository.GetUserByEmail(dto.Email) is not null)
        {
            return new RegisterResponseDTO { Success = false, Message = "User already exits" };
        }
        else
        {
            var user = new User
            {
                Email = dto.Email,
                EmailVerified = false,
                Roles = dto.Roles,
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
            await _userRepository.AddUserAsync(user);

            return new RegisterResponseDTO { Success = true, Message = "Successfully registered user" };
        }
    }
}
