namespace ReservaConEnanos.Models;

public class AvailableSession 
{
    public int Id { get; set; }
    public DateTime SessionDate { get; set; }
    public DateTime Duration { get; set; }
    public EscapeRoom? EscapeRoom { get; set; }
    public Reservation? Reservation { get; set; }
    public int TotalPrice { get; set; } = 0;
    public int ReservationPrice { get; set; } = 0;
}