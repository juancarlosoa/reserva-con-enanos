using Microsoft.AspNetCore.Mvc;
using RCE_Providers.EscapeRoomProviders.DTOs;
using RCE_Providers.EscapeRoomProviders.Services;

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

        [HttpGet("{providerSlug}")]
        public async Task<ActionResult<EscapeRoomProviderResponseDTO>> GetProviderBySlug(string providerSlug)
        {
            var provider = await _providerService.GetProviderBySlugAsync(providerSlug);
            if (provider == null) return NotFound();
            return Ok(provider);
        }

        [HttpPost]
        public async Task<ActionResult<EscapeRoomProviderResponseDTO>> CreateProvider([FromBody] EscapeRoomProviderRequestDTO dto)
        {
            var created = await _providerService.CreateProviderAsync(dto);
            return CreatedAtAction(
                nameof(GetProviderBySlug),
                new { providerSlug = created.Slug },
                created
            );
        }

        [HttpPut("{providerSlug}")]
        public async Task<ActionResult<EscapeRoomProviderResponseDTO>> UpdateProvider(string providerSlug, [FromBody] EscapeRoomProviderRequestDTO dto)
        {
            var result = await _providerService.UpdateProviderBySlugAsync(providerSlug, dto);
            if (!result) return NotFound();

            return Ok();
        }

        [HttpDelete("{providerSlug}")]
        public async Task<IActionResult> DeleteProvider(string providerSlug)
        {
            var success = await _providerService.DeleteProviderBySlugAsync(providerSlug);
            if (!success) return NotFound();

            return Ok();
        }
    }
}