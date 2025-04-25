namespace ReservaConEnanos.Reservations.Entities;

public class Reservation
{
    public Guid Id { get; set; }
    public Guid EscapeRoomId { get; set; }
    public DateTime SessionDate { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public int PlayersCount { get; set; }
    public int Paid { get; set; }
    public string? Notes { get; set; }
}