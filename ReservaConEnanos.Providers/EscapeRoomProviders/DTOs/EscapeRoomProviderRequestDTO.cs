using System;

namespace ReservaConEnanos.Providers.EscapeRoomProviders.DTOs;

public class EscapeRoomProviderRequestDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
