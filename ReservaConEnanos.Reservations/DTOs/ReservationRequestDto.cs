namespace ReservaConEnanos.Reservations.DTOs;

public class ReservationRequestDto
{
    public DateTime SessionDate { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public int PlayersCount { get; set; }
    public string Notes { get; set; } = string.Empty;
    public int Paid { get; set; } = 0;
}
