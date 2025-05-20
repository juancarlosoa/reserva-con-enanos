namespace ReservaConEnanos.Frontend.EscapeRoomProviders.Entities;

public class EscapeRoomProvider
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string ImageUrl { get ; set ; } = string.Empty;
    public string City { get ; set ; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}