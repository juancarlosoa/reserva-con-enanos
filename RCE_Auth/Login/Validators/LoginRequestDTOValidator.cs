using FluentValidation;
using RCE_Auth.Login.DTOs;
using RCE_Auth.Shared.Validators;

namespace RCE_Auth.Login.Validators
{
    public class LoginRequestDTOValidator : AbstractValidator<LoginRequestDTO>
    {
        public LoginRequestDTOValidator() 
        { 
            RuleFor(x => x.Email).ApplyEmailRules();

            RuleFor(x => x.Password).ApplyPasswordRules();


        }
    }
}
