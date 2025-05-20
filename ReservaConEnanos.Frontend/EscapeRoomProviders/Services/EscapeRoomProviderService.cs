using ReservaConEnanos.Frontend.EscapeRoomProviders.DTOs;
using ReservaConEnanos.Frontend.EscapeRoomProviders.Entities;
using ReservaConEnanos.Frontend.EscapeRoomProviders.ApiClients;
using AutoMapper;

namespace ReservaConEnanos.Frontend.EscapeRoomProviders.Services;

public class EscapeRoomProviderService : IEscapeRoomProviderService
{
    private readonly IEscapeRoomProviderApiClient _providerApiClient;
    private readonly IMapper _mapper;

    public EscapeRoomProviderService(IEscapeRoomProviderApiClient providerApiClient, IMapper mapper)
    {
        _providerApiClient = providerApiClient;
        _mapper = mapper;
    }

    public async Task<EscapeRoomProvider?> CreateProviderAsync(EscapeRoomProvider provider)
    {
        var dto = _mapper.Map<EscapeRoomProviderCreateDTO>(provider);
        var createdDto = await _providerApiClient.CreateProviderAsync(dto);

        return _mapper.Map<EscapeRoomProvider>(createdDto);
    }

    public async Task<bool> DeleteProviderAsync(Guid id)
    {
        return await _providerApiClient.DeleteProviderAsync(id);
    }

    public async Task<IEnumerable<EscapeRoomProvider>> GetAllProvidersAsync()
    {
        var providersDto = (await _providerApiClient.GetAllProvidersAsync())?.ToList();
        if (providersDto is null) providersDto = [];
        
        return _mapper.Map<IEnumerable<EscapeRoomProvider>>(providersDto);
    }

    public async Task<EscapeRoomProvider?> GetProviderByIdAsync(Guid id)
    {
        var dto = await _providerApiClient.GetProviderByIdAsync(id);

        return _mapper.Map<EscapeRoomProvider>(dto);
    }

    public async Task<EscapeRoomProvider?> UpdateProviderAsync(EscapeRoomProvider provider)
    {
        var dto = new EscapeRoomProviderUpdateDTO();
        var updatedDto = await _providerApiClient.UpdateProviderAsync(dto);

        return _mapper.Map<EscapeRoomProvider>(updatedDto);
    }
}