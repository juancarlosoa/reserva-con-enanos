using Microsoft.AspNetCore.Mvc;
using ReservaConEnanos.Providers.EscapeRoomProviders.DTOs;
using ReservaConEnanos.Providers.EscapeRoomProviders.Services;
using ReservaConEnanos.Providers.Rooms.DTOs;

namespace ReservaConEnanos.Providers.EscapeRoomProviders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscapeRoomProviderController : ControllerBase
    {
        private readonly IEscapeRoomProviderService _providerService;

        public EscapeRoomProviderController(IEscapeRoomProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpGet]
        public async Task<
            ActionResult<IEnumerable<EscapeRoomProviderResponseDTO>>
        > GetAllProviders()
        {
            var providers = await _providerService.GetAllProviders();
            return Ok(providers);
        }

        [HttpGet("{providerId}")]
        public async Task<ActionResult<EscapeRoomProviderResponseDTO>> GetProviderById(Guid providerId)
        {
            var provider = await _providerService.GetProviderByIdAsync(providerId);
            if (provider == null) return NotFound();

            return Ok(provider);
        }

        [HttpPost]
        public async Task<ActionResult<EscapeRoomProviderResponseDTO>> CreateProvider([FromBody] EscapeRoomProviderCreateDTO dto)
        {
            var created = await _providerService.CreateProviderAsync(dto);
            return CreatedAtAction(
                nameof(GetProviderById),
                new { providerId = created.Id },
                created
            );
        }

        [HttpPut]
        public async Task<ActionResult<EscapeRoomProviderResponseDTO>> UpdateProvider([FromBody] EscapeRoomProviderUpdateDTO dto)
        {
            var updated = await _providerService.UpdateProviderAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{providerId}")]
        public async Task<IActionResult> DeleteProvider(Guid providerId)
        {
            var success = await _providerService.DeleteProvider(providerId);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{providerId}/rooms")]
        public async Task<ActionResult<IEnumerable<RoomResponseDTO>>> GetRoomsByProvider(Guid providerId)
        {
            var rooms = await _providerService.GetRoomsByProviderIdAsync(providerId);

            return Ok(rooms);
        }
    }
}