using ReservaConEnanos.Frontend.EscapeRoomProviders.DTOs;
using ReservaConEnanos.Frontend.Http;

namespace ReservaConEnanos.Frontend.EscapeRoomProviders.ApiClients;

public class EscapeRoomProviderApiClient : IEscapeRoomProviderApiClient
{
    private const string BaseRoute = "api/EscapeRoomProvider";
    private readonly BaseHttpService _httpService;

    public EscapeRoomProviderApiClient(BaseHttpService httpClient)
    {
        _httpService = httpClient;
    }

    public Task<EscapeRoomProviderResponseDTO?> CreateProviderAsync(EscapeRoomProviderCreateDTO dto)
    {
        return _httpService.PostAsync<EscapeRoomProviderResponseDTO>(BaseRoute, dto);
    }

    public Task<bool> DeleteProviderAsync(Guid id)
    {
        return _httpService.DeleteAsync($"{BaseRoute}/{id}");
    }

    public Task<IEnumerable<EscapeRoomProviderResponseDTO>?> GetAllProvidersAsync()
    {
        return _httpService.GetAsync<IEnumerable<EscapeRoomProviderResponseDTO>>(BaseRoute);
    }

    public Task<EscapeRoomProviderResponseDTO?> GetProviderByIdAsync(Guid id)
    {
        return _httpService.GetAsync<EscapeRoomProviderResponseDTO?>($"{BaseRoute}/{id}");
    }

    public Task<EscapeRoomProviderResponseDTO?> UpdateProviderAsync(EscapeRoomProviderUpdateDTO dto)
    {
        return _httpService.PutAsync<EscapeRoomProviderResponseDTO>(BaseRoute, dto);
    }
}
