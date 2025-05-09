using AutoMapper;
using ReservaConEnanos.Providers.EscapeRoomProviders.DTOs;
using ReservaConEnanos.Providers.EscapeRoomProviders.Entities;
using ReservaConEnanos.Providers.EscapeRoomProviders.Repositories;
using ReservaConEnanos.Providers.Rooms.DTOs;

namespace ReservaConEnanos.Providers.EscapeRoomProviders.Services;

public class EscapeRoomProviderService : IEscapeRoomProviderService
{
    private readonly IEscapeRoomProviderRepository _repository;
    private readonly IMapper _mapper;

    public EscapeRoomProviderService(IEscapeRoomProviderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<EscapeRoomProviderResponseDTO> CreateProviderAsync(EscapeRoomProviderCreateDTO dto)
    {
        var provider = _mapper.Map<EscapeRoomProvider>(dto);
        await _repository.AddAsync(provider);

        return _mapper.Map<EscapeRoomProviderResponseDTO>(provider);
    }

    public async Task<bool> DeleteProvider(Guid providerId)
    {
        return await _repository.DeleteAsync(providerId);
    }

    public async Task<IEnumerable<EscapeRoomProviderResponseDTO>> GetAllProviders()
    {
        var providers = await _repository.GetAllAsync();

        return _mapper.Map<IEnumerable<EscapeRoomProviderResponseDTO>>(providers);
    }

    public async Task<EscapeRoomProviderResponseDTO?> GetProviderByIdAsync(Guid providerId)
    {
        var provider = await _repository.GetByIdAsync(providerId);

        return _mapper.Map<EscapeRoomProviderResponseDTO>(provider);
    }

    public async Task<IEnumerable<RoomResponseDTO>> GetRoomsByProviderIdAsync(Guid providerId)
    {
        var rooms = await _repository.GetRoomsByProviderIdAsync(providerId);

        return _mapper.Map<IEnumerable<RoomResponseDTO>>(rooms);
    }

    public async Task<EscapeRoomProviderResponseDTO> UpdateProviderAsync(EscapeRoomProviderUpdateDTO dto)
    {
        var provider = _mapper.Map<EscapeRoomProvider>(dto);
        await _repository.UpdateAsync(provider);

        return _mapper.Map<EscapeRoomProviderResponseDTO>(provider);
    }
}