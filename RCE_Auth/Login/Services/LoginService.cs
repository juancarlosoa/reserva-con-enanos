using Microsoft.AspNetCore.Identity;
using RCE_Auth.Login.DTOs;
using RCE_Auth.Tokens.Services;
using RCE_Auth.Users.Entities;
using RCE_Auth.Users.Repositories;

namespace RCE_Auth.Login.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenProvider _tokenProvider;

        public LoginService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, ITokenProvider tokenProvider)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenProvider = tokenProvider;
        }

        public Task<LoginResponseDTO> AuthenticateAsync(LoginRequestDTO dto)
        {
            var user = _userRepository.GetUserByEmail(dto.Email);

            if (user is null)
            {
                return Task.FromResult(new LoginResponseDTO
                {
                    Token = "error",
                    Expiration = DateTime.UtcNow
                });
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result != PasswordVerificationResult.Success)
            {
                return Task.FromResult(new LoginResponseDTO
                {
                    Token = "error",
                    Expiration = DateTime.UtcNow
                });
            }
            var token = _tokenProvider.GenerateToken(user);

            return Task.FromResult(new LoginResponseDTO
            {
                Token = token.Token,
                Expiration = token.ExpiresAt,
                UserId = user.Id,
                UserRoles = user.Roles,
                EmailVerified = user.EmailVerified
            });
        }
    }
}
