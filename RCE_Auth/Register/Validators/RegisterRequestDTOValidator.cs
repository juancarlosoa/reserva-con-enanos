using FluentValidation;
using RCE_Auth.Shared.Validators;
using RCE_Auth.Register.DTOs;

namespace RCE_Auth.Register.Validators;

public class RegisterRequestDTOValidator : AbstractValidator<RegisterRequestDTO>
{
    public RegisterRequestDTOValidator()
    {
        RuleFor(x => x.Email).ApplyEmailRules();

        RuleFor(x => x.Password).ApplyPasswordRules();

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage("Passwords do not match");
    }
}
