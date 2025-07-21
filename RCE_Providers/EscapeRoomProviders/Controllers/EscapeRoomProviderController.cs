using Microsoft.AspNetCore.Mvc;
using RCE_Providers.EscapeRoomProviders.DTOs;
using RCE_Providers.EscapeRoomProviders.Services;
using RCE_Providers.Rooms.DTOs;

namespace RCE_Providers.EscapeRoomProviders.Controllers
{
    [Route("providers")]
    [ApiController]
    public class EscapeRoomProviderController : ControllerBase
    {
        private readonly IEscapeRoomProviderService _providerService;

        public EscapeRoomProviderController(IEscapeRoomProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EscapeRoomProviderResponseDTO>>> GetAllProviders()
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

        [HttpPut("{providerId}")]
        public async Task<ActionResult<EscapeRoomProviderResponseDTO>> UpdateProvider(Guid providerId, [FromBody] EscapeRoomProviderUpdateDTO dto)
        {
            var result = await _providerService.UpdateProviderAsync(providerId, dto);
            if (!result) return NotFound();

            return Ok();
        }

        [HttpDelete("{providerId}")]
        public async Task<IActionResult> DeleteProvider(Guid providerId)
        {
            var success = await _providerService.DeleteProvider(providerId);
            if (!success) return NotFound();

            return Ok();
        }

        [HttpGet("{providerId}/rooms")]
        public async Task<ActionResult<IEnumerable<RoomResponseDTO>>> GetRoomsByProvider(Guid providerId)
        {
            var rooms = await _providerService.GetRoomsByProviderIdAsync(providerId);

            return Ok(rooms);
        }
    }
}