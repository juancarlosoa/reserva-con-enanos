namespace RCE_Providers.Rooms.DTOs;

public class RoomResponseDTO
{
    public Guid Id { get; set; }
    public Guid ProviderId { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty;
    public required int MinPlayers { get; set; }
    public required int MaxPlayers { get; set; }
    public DateTime CreatedAt { get; set; }
    public int DurationMinutes { get; set; }
}