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

        return _mapper.Map<RoomResponseDTO>(newRoom);
    }

    public async Task<RoomResponseDTO?> GetRoomByIdAsync(Guid id)
    {
        var room = await _repository.GetByIdAsync(id);

        return _mapper.Map<RoomResponseDTO>(room);
    }

    public async Task<RoomResponseDTO> UpdateRoomAsync(RoomRequestDTO dto)
    {
        var room = _mapper.Map<Room>(dto);

        await _repository.UpdateAsync(room);

        return _mapper.Map<RoomResponseDTO>(room);
    }

    public async Task<bool> DeleteRoom(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }
}