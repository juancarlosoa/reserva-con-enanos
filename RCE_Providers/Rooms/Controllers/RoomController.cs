using Microsoft.AspNetCore.Mvc;
using RCE_Providers.Rooms.DTOs;
using RCE_Providers.Rooms.Services;

namespace RCE_Providers.Rooms.Controllers
{
    [Route("rooms")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService service)
        {
            _roomService = service;
        }

        [HttpGet("{roomId}")]
        public async Task<ActionResult<RoomResponseDTO>> GetRoomById(Guid roomId)
        {
            var room = await _roomService.GetRoomByIdAsync(roomId);
            if (room == null) return NotFound();

            return Ok(room);
        }

        [HttpPost]
        public async Task<ActionResult<RoomResponseDTO>> CreateRoom([FromBody] RoomRequestDTO dto)
        {
            var createdRoom = await _roomService.CreateRoomAsync(dto);
            return CreatedAtAction(
                nameof(GetRoomById),
                new { roomId = createdRoom.Id },
                createdRoom
                );
        }

        [HttpPut]
        public async Task<ActionResult<RoomResponseDTO>> UpdateRoom([FromBody] RoomRequestDTO dto)
        {
            var updatedRoom = await _roomService.UpdateRoomAsync(dto);
            return Ok(updatedRoom);
        }

        [HttpDelete("{roomId}")]
        public async Task<IActionResult> DeleteRoom(Guid roomId)
        {
            var success = await _roomService.DeleteRoom(roomId);
            if (!success) return NotFound();

            return Ok();
        }
    }
}
