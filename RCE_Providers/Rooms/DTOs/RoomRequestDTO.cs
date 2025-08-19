namespace RCE_Providers.Rooms.DTOs;

public class RoomRequestDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty;
    public required int MinPlayers { get; set; }
    public required int MaxPlayers { get; set; }
    public int DurationMinutes { get; set; } = 0;
}
