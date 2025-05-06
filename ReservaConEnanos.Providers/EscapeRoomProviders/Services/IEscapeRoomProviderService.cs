using System;
using ReservaConEnanos.Providers.EscapeRoomProviders.DTOs;
using ReservaConEnanos.Providers.Rooms.DTOs;

namespace ReservaConEnanos.Providers.EscapeRoomProviders.Services;

public interface IEscapeRoomProviderService
{
    Task<IEnumerable<EscapeRoomProviderResponseDTO>> GetAllProviders();
    Task<EscapeRoomProviderResponseDTO?> GetProviderByIdAsync(Guid providerId);
    Task<EscapeRoomProviderResponseDTO> CreateProviderAsync(EscapeRoomProviderRequestDTO dto);
    Task<EscapeRoomProviderResponseDTO> UpdateProviderAsync(EscapeRoomProviderRequestDTO dto);
    Task<bool> DeleteProvider(Guid providerId);
    Task<IEnumerable<RoomResponseDTO>> GetRoomsByProviderIdAsync(Guid providerId);
}