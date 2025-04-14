namespace ReservaConEnanos.Models;

public class EscapeRoomProvider
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email {get; set; } = string.Empty;
    public List<EscapeRoom> EscapeRooms { get; set; } = new();
} 