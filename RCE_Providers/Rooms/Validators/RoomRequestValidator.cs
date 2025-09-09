using FluentValidation;
using RCE_Providers.Rooms.DTOs;

namespace RCE_Providers.Rooms.Validators;

public class RoomRequestValidator : AbstractValidator<RoomRequestDTO>
{
    public RoomRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre de la sala es requerido")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres")
            .MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("La descripción no puede exceder 1000 caracteres");

        RuleFor(x => x.Theme)
            .NotEmpty().WithMessage("El tema es requerido")
            .MaximumLength(50).WithMessage("El tema no puede exceder 50 caracteres");

        RuleFor(x => x.MinPlayers)
            .GreaterThan(0).WithMessage("El número mínimo de jugadores debe ser mayor a 0")
            .LessThanOrEqualTo(x => x.MaxPlayers).WithMessage("El mínimo de jugadores no puede ser mayor al máximo");

        RuleFor(x => x.MaxPlayers)
            .GreaterThan(0).WithMessage("El número máximo de jugadores debe ser mayor a 0")
            .LessThanOrEqualTo(20).WithMessage("El máximo de jugadores no puede exceder 20");

        RuleFor(x => x.DurationMinutes)
            .GreaterThan(0).WithMessage("La duración debe ser mayor a 0 minutos")
            .LessThanOrEqualTo(300).WithMessage("La duración no puede exceder 300 minutos");
    }
}