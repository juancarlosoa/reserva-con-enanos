using AutoMapper;
using RCE_Providers.Rooms.DTOs;
using RCE_Providers.Rooms.Entities;
using RCE_Providers.Rooms.Repositories;

namespace RCE_Providers.Rooms.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _repository;
    private readonly IMapper _mapper;

    public RoomService(IRoomRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<RoomResponseDTO> CreateRoomAsync(RoomRequestDTO dto)
    {
        var newRoom = _mapper.Map<Room>(dto);
        await _repository.AddAsync(newRoom);
        await _repository.SaveChangesAsync();

        return _mapper.Map<RoomResponseDTO>(newRoom);
    }

    public async Task<RoomResponseDTO?> GetRoomByIdAsync(Guid id)
    {
        var room = await _repository.GetByIdAsync(id);

        return _mapper.Map<RoomResponseDTO>(room);
    }

    public async Task<bool> UpdateRoomAsync(Guid providerId, RoomRequestDTO dto)
    {
        var room = await _repository.GetByIdAsync(providerId);
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

    public async Task<bool> DeleteRoomAsync(Guid roomId)
    {
        var room = await _repository.GetByIdAsync(roomId);
        if (room == null) return false;
        try
        {
            _repository.Delete(room);
            await _repository.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }
}