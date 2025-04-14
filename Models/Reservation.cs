namespace ReservaConEnanos.Models;

public class Reservation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone {get; set; } = string.Empty;
    public int AvailableSessionId { get; set; }
    public required AvailableSession Session { get; set; }
    public int Paid { get; set; } = 0;
    public int? Discount { get; set; }
    public string? DiscountCode { get; set; }
}