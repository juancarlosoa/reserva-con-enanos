namespace ReservaConEnanos.Models;

public class EscapeRoom
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Direction {get; set; } = string.Empty;
    public int EscapeRoomProviderId { get; set; }
    public EscapeRoomProvider? Provider { get; set; }
    public List<AvailableSession> Sessions { get; set; } = new();
}