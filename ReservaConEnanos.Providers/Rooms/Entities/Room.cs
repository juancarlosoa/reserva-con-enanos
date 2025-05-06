using ReservaConEnanos.Providers.EscapeRoomProviders.Entities;

namespace ReservaConEnanos.Providers.Rooms.Entities;

public class Room
{
    public Guid Id { get; set; }
    public Guid ProviderId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty;
    public required int MinPlayers { get; set; }
    public required int MaxPlayers { get; set; }
    public int DurationMinutes { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public EscapeRoomProvider? Provider { get; set; }
}
