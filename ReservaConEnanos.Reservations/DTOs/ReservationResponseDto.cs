namespace ReservaConEnanos.Reservations.DTOs;

public class ReservationResponseDto
{
    public Guid Id { get; set; }
    public DateTime SessionDate { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public int Paid { get; set; } = 0;
}
