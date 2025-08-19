import type { Room } from '@/models/Room'
import type { RoomRequestDTO, RoomResponseDTO } from './room.dto'

export function mapRoomResponseToModel(dto: RoomResponseDTO): Room {
    return {
        id: dto.id,
        name: dto.name,
        capacity: dto.capacity,
        type: dto.type,
        hourlyRate: dto.hourlyRate,
        available: dto.available,
        providerId: dto.providerId
    }
}

export function mapRoomModelToRequest(model: Room): RoomRequestDTO {
    return {
        name: model.name,
        capacity: model.capacity,
        type: model.type,
        hourlyRate: model.hourlyRate,
        available: model.available,
        providerId: model.providerId
    }
}


