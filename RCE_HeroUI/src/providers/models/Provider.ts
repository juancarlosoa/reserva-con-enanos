import { ProviderResponseDTO } from "../dtos/Providers/ProviderResponseDTO";
import { Room } from "../../rooms/models/Room";
import { RoomResponseDTO } from "../../rooms/dtos/RoomResponseDTO";

export class Provider {
    id: string;
    name: string;
    email: string;
    phoneNumber: string;
    createdAt: string;
    rooms: Room[];

    constructor(dto: ProviderResponseDTO) {
        this.id = dto.id;
        this.name = dto.name;
        this.email = dto.email;
        this.phoneNumber = dto.phoneNumber;
        this.createdAt = dto.createdAt;
        this.rooms = (dto.rooms ?? []).map((roomDto: RoomResponseDTO) => new Room(roomDto));
    }
}