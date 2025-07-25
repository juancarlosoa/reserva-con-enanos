import { REPOSITORY_HOSTS } from "../../api/repositoryHosts";
import { apiAdapter } from "@/api";
import { RoomResponseDTO } from "../dtos/RoomResponseDTO";
import { RoomRequestDTO } from "../dtos/RoomRequestDTO";

export const roomRepository = {
    
    getRoomById: async (roomId: string) => {
        return apiAdapter.get<RoomResponseDTO>(`${REPOSITORY_HOSTS.ROOMS}/${roomId}`);
    },

    createRoom: async (data: RoomRequestDTO) => {
        return apiAdapter.post<RoomResponseDTO>(`${REPOSITORY_HOSTS.ROOMS}`, data)
    },

    deleteRoom: async (roomId: string) => {
        return apiAdapter.delete(`${REPOSITORY_HOSTS.ROOMS}/${roomId}`);
    },

    updateProvider: async (roomId: string, data: RoomRequestDTO) => {
        return apiAdapter.put<RoomResponseDTO>(`${REPOSITORY_HOSTS.ROOMS}/${roomId}`, data);
    },
};