using Microsoft.AspNetCore.Mvc;
using RCE_Providers.Rooms.DTOs;
using RCE_Providers.Rooms.Services;

namespace RCE_Providers.Rooms.Controllers
{
    [Route("providers/{providerSlug}/rooms")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService service)
        {
            _roomService = service;
        }

        [HttpGet(Name = "GetRoomsByProviderSlug")]
        public async Task<ActionResult<IEnumerable<RoomResponseDTO>>> GetRoomsByProviderSlug(string providerSlug)
        {
            var rooms = await _roomService.GetRoomsByProviderSlugAsync(providerSlug);
            return Ok(rooms);
        }

        [HttpGet("{roomSlug}")]
        public async Task<ActionResult<RoomResponseDTO>> GetRoomBySlugs(string providerSlug, string roomSlug)
        {
            var room = await _roomService.GetRoomBySlugsAsync(providerSlug, roomSlug);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [HttpPost]
        public async Task<ActionResult<RoomResponseDTO>> CreateRoomByProviderSlug(string providerSlug, [FromBody] RoomRequestDTO dto)
        {
            var createdRoom = await _roomService.CreateRoomByProviderSlugAsync(providerSlug, dto);
            if (createdRoom == null) return NotFound();
            return CreatedAtAction(
                nameof(GetRoomBySlugs),
                new { providerSlug, roomSlug = createdRoom.Slug },
                createdRoom
            );
        }

        [HttpPut("{roomSlug}")]
        public async Task<IActionResult> UpdateRoomBySlugs(string providerSlug, string roomSlug, [FromBody] RoomRequestDTO dto)
        {
            var ok = await _roomService.UpdateRoomBySlugsAsync(providerSlug, roomSlug, dto);
            if (!ok) return NotFound();
            return Ok();
        }

        [HttpDelete("{roomSlug}")]
        public async Task<IActionResult> DeleteRoomBySlugs(string providerSlug, string roomSlug)
        {
            var ok = await _roomService.DeleteRoomBySlugsAsync(providerSlug, roomSlug);
            if (!ok) return NotFound();
            return Ok();
        }
    }
}
