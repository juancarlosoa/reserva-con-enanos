using ReservaConEnanos.Providers.Rooms.Entities;

namespace ReservaConEnanos.Providers.EscapeRoomProviders.Entities;

public class EscapeRoomProvider
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Room> Rooms { get; set; } = [];
}
