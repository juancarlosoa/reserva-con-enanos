using Microsoft.AspNetCore.Mvc;
using ReservaConEnanos.Reservations.Services.Interfaces;
using ReservaConEnanos.Reservations.DTOs;

namespace ReservaConEnanos.Reservations.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet("{escapeRoomId}/monthly")] 
    public async Task<ActionResult<IEnumerable<ReservationResponseDto>>> GetMonthlyReservations(Guid escapeRoomId, [FromQuery] DateTime month)
    {
        var reservations = await _reservationService.GetMonthlyReservationsAsync(escapeRoomId, month);
        return Ok(reservations);
    }

    [HttpPost]
    public async Task<ActionResult<ReservationResponseDto>> CreateReservation([FromBody] ReservationRequestDto reservationDto)
    {
        var result = await _reservationService.CreateReservationAsync(reservationDto);
        return CreatedAtAction(nameof(GetReservationById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReservationResponseDto>> GetReservationById(Guid id)
    {
        var reservation = await _reservationService.GetReservationByIdAsync(id);
        if (reservation == null) return NotFound();
        return Ok(reservation);
    }
}