import { RoomResponseDTO } from "@/rooms/dtos/RoomResponseDTO";

export interface ProviderResponseDTO {
    id: string;
    name: string;
    email: string;
    phoneNumber: string;
    createdAt: string;
    rooms: RoomResponseDTO[];
}