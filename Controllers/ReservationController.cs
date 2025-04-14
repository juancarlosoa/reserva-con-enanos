using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservaConEnanos.CoreData;
using ReservaConEnanos.Models;

namespace ReservaConEnanos.API;

[ApiController]
[Route("[controller]")]

public class ReservationController: ControllerBase
{
    private readonly AppDbContext _context;

    public ReservationController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public string GetReservations() {
        return "er";
    }
}