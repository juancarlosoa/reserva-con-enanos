using AutoMapper;
using RCE_Providers.Rooms.DTOs;
using RCE_Providers.Rooms.Entities;
using RCE_Providers.Rooms.Repositories;
using RCE_Providers.EscapeRoomProviders.Repositories;
using RCE_Providers.Common;

namespace RCE_Providers.Rooms.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _repository;
    private readonly IEscapeRoomProviderRepository _providerRepository;
    private readonly IMapper _mapper;

    public RoomService(IRoomRepository repository, IEscapeRoomProviderRepository providerRepository, IMapper mapper)
    {
        _repository = repository;
        _providerRepository = providerRepository;
        _mapper = mapper;
    }

    public async Task<RoomResponseDTO?> CreateRoomByProviderSlugAsync(string providerSlug, RoomRequestDTO dto)
    {
        var provider = await _providerRepository.GetBySlugAsync(providerSlug);
        if (provider == null) return null;
        var newRoom = _mapper.Map<Room>(dto);
        newRoom.Slug = await GenerateUniqueRoomSlug(provider.Id, dto.Name);
        newRoom.ProviderId = provider.Id;
        await _repository.AddAsync(newRoom);
        await _repository.SaveChangesAsync();
        return _mapper.Map<RoomResponseDTO>(newRoom);
    }

    public async Task<RoomResponseDTO?> GetRoomBySlugsAsync(string providerSlug, string roomSlug)
    {
        var provider = await _providerRepository.GetBySlugAsync(providerSlug);
        if (provider == null) return null;
        var room = await _repository.GetByProviderAndSlugAsync(provider.Id, roomSlug);
        return _mapper.Map<RoomResponseDTO>(room);
    }

    public async Task<bool> UpdateRoomBySlugsAsync(string providerSlug, string roomSlug, RoomRequestDTO dto)
    {
        var provider = await _providerRepository.GetBySlugAsync(providerSlug);
        if (provider == null) return false;
        var room = await _repository.GetByProviderAndSlugAsync(provider.Id, roomSlug);
        if (room == null) return false;
        try
        {
            _repository.Update(_mapper.Map(dto, room));
            await _repository.SaveChangesAsync();
            return true;
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> DeleteRoomBySlugsAsync(string providerSlug, string roomSlug)
    {
        var provider = await _providerRepository.GetBySlugAsync(providerSlug);
        if (provider == null) return false;
        var room = await _repository.GetByProviderAndSlugAsync(provider.Id, roomSlug);
        if (room == null) return false;
        try
        {
            // Soft delete
            room.IsDeleted = true;
            room.DeletedAt = DateTime.UtcNow;
            _repository.Update(room);
            await _repository.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<RoomResponseDTO>> GetRoomsByProviderSlugAsync(string providerSlug)
    {
        var provider = await _providerRepository.GetBySlugAsync(providerSlug);
        if (provider == null) return Array.Empty<RoomResponseDTO>();
        var rooms = await _providerRepository.GetRoomsByProviderIdAsync(provider.Id);
        return _mapper.Map<IEnumerable<RoomResponseDTO>>(rooms);
    }

    private async Task<string> GenerateUniqueRoomSlug(Guid providerId, string name)
    {
        var baseSlug = Slug.Generate(name);
        if (string.IsNullOrWhiteSpace(baseSlug))
        {
            baseSlug = "room";
        }
        var candidate = baseSlug;
        var suffix = 2;
        while (await _repository.GetByProviderAndSlugAsync(providerId, candidate) != null)
        {
            candidate = $"{baseSlug}-{suffix}";
            suffix++;
        }
        return candidate;
    }
}