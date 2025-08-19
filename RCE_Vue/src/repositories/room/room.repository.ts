import { apiAdapter } from '@/core/http/apiAdapter'
import { REPOSITORY_HOSTS } from '@/core/constants/repositoryHosts'
import type { Room } from '@/models/Room'
import type { RoomRequestDTO, RoomResponseDTO } from './room.dto'
import { mapRoomModelToRequest, mapRoomResponseToModel } from './room.mapper'

export const roomRepository = {
    async getRoomById(roomId: string): Promise<Room> {
        const dto = await apiAdapter.get<RoomResponseDTO>(`${REPOSITORY_HOSTS.ROOMS}/${roomId}`)
        return mapRoomResponseToModel(dto)
    },

    async createRoom(model: Room): Promise<Room> {
        const payload: RoomRequestDTO = mapRoomModelToRequest(model)
        const dto = await apiAdapter.post<RoomResponseDTO, RoomRequestDTO>(`${REPOSITORY_HOSTS.ROOMS}`, payload)
        return mapRoomResponseToModel(dto)
    },

    async deleteRoom(roomId: string): Promise<void> {
        await apiAdapter.delete(`${REPOSITORY_HOSTS.PROVIDERS}/${REPOSITORY_HOSTS.ROOMS}/${roomId}`)
    },

    async updateRoom(roomId: string, model: Room): Promise<Room> {
        const payload: RoomRequestDTO = mapRoomModelToRequest(model)
        const dto = await apiAdapter.put<RoomResponseDTO, RoomRequestDTO>(`${REPOSITORY_HOSTS.ROOMS}/${roomId}`, payload)
        return mapRoomResponseToModel(dto)
    }
}


