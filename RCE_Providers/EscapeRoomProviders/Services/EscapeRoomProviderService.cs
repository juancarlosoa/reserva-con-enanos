using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RCE_Providers.EscapeRoomProviders.DTOs;
using RCE_Providers.EscapeRoomProviders.Entities;
using RCE_Providers.EscapeRoomProviders.Repositories;
using RCE_Providers.Rooms.DTOs;

namespace RCE_Providers.EscapeRoomProviders.Services;

public class EscapeRoomProviderService : IEscapeRoomProviderService
{
    private readonly IEscapeRoomProviderRepository _repository;
    private readonly IMapper _mapper;

    public EscapeRoomProviderService(IEscapeRoomProviderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<EscapeRoomProviderResponseDTO> CreateProviderAsync(EscapeRoomProviderRequestDTO dto)
    {
        var provider = _mapper.Map<EscapeRoomProvider>(dto);
        await _repository.AddAsync(provider);
        await _repository.SaveChangesAsync();

        return _mapper.Map<EscapeRoomProviderResponseDTO>(provider);
    }

    public async Task<bool> DeleteProviderAsync(Guid providerId)
    {
        var provider = await _repository.GetByIdAsync(providerId);
        if (provider == null) return false;
        try
        {
            _repository.Delete(provider);
            await _repository.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> UpdateProviderAsync(Guid providerId, EscapeRoomProviderRequestDTO dto)
    {
        var provider = await _repository.GetByIdAsync(providerId);
        if (provider == null) return false;
        try
        {
            _repository.Update(_mapper.Map(dto, provider));
            await _repository.SaveChangesAsync();

            return true;
        }
        catch
        {
            throw;
        }
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
}