using RCE_Providers.Rooms.Entities;

namespace RCE_Providers.EscapeRoomProviders.Entities;

public class EscapeRoomProvider
{
    public Guid Id { get; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Room> Rooms { get; set; } = [];
}
