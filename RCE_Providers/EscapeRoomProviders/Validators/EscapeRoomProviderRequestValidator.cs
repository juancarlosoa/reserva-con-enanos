using FluentValidation;
using RCE_Providers.EscapeRoomProviders.DTOs;

namespace RCE_Providers.EscapeRoomProviders.Validators;

public class EscapeRoomProviderRequestValidator : AbstractValidator<EscapeRoomProviderRequestDTO>
{
    public EscapeRoomProviderRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres")
            .MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es requerido")
            .EmailAddress().WithMessage("El formato del email no es válido")
            .MaximumLength(255).WithMessage("El email no puede exceder 255 caracteres");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("El número de teléfono es requerido")
            .Matches(@"^\+?[1-9]\d{8,14}$").WithMessage("El formato del teléfono no es válido");
    }
}