using RCE_Providers.Rooms.DTOs;

namespace RCE_Providers.EscapeRoomProviders.DTOs;

public class EscapeRoomProviderResponseDTO
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public RoomResponseDTO[] Rooms { get; set; } = [];
}
