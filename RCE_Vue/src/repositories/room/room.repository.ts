import { apiAdapter } from '@/core/http/apiAdapter'
import { REPOSITORY_HOSTS } from '@/core/constants/repositoryHosts'
import type { Room } from '@/models/Room'
import type { RoomRequestDTO, RoomResponseDTO } from './room.dto'
import { mapRoomModelToRequest, mapRoomResponseToModel } from './room.mapper'

export const roomRepository = {
    async getRoomBySlug(providerSlug: string, roomSlug: string): Promise<Room> {
        const dto = await apiAdapter.get<RoomResponseDTO>(`${REPOSITORY_HOSTS.PROVIDERS}/${providerSlug}${REPOSITORY_HOSTS.ROOMS}/${roomSlug}`)
        return mapRoomResponseToModel(dto)
    },

    async createRoom(providerSlug: string, model: Room): Promise<Room> {
        const payload: RoomRequestDTO = mapRoomModelToRequest(model)
        const dto = await apiAdapter.post<RoomResponseDTO, RoomRequestDTO>(`${REPOSITORY_HOSTS.PROVIDERS}/${providerSlug}${REPOSITORY_HOSTS.ROOMS}`, payload)
        return mapRoomResponseToModel(dto)
    },

    async deleteRoom(providerSlug: string, roomSlug: string): Promise<void> {
        await apiAdapter.delete(`${REPOSITORY_HOSTS.PROVIDERS}/${providerSlug}${REPOSITORY_HOSTS.ROOMS}/${roomSlug}`)
    },

    async updateRoom(providerSlug: string, roomSlug: string, model: Room): Promise<Room> {
        const payload: RoomRequestDTO = mapRoomModelToRequest(model)
        const dto = await apiAdapter.put<RoomResponseDTO, RoomRequestDTO>(`${REPOSITORY_HOSTS.PROVIDERS}/${providerSlug}${REPOSITORY_HOSTS.ROOMS}/${roomSlug}`, payload)
        return mapRoomResponseToModel(dto)
    }
}


