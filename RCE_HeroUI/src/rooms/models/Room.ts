import { RoomResponseDTO } from "../dtos/RoomResponseDTO";

export class Room {
  id: string;
  providerId: string;
  name: string;
  description?: string;
  theme?: string;
  minPlayers: number;
  maxPlayers: number;
  durationMinutes?: number;
  createdAt?: string;

  constructor(dto: RoomResponseDTO) {
    this.id = dto.id;
    this.providerId = dto.providerId;
    this.name = dto.name;
    this.description = dto.description;
    this.theme = dto.theme;
    this.minPlayers = dto.minPlayers;
    this.maxPlayers = dto.maxPlayers;
    this.durationMinutes = dto.durationMinutes;
    this.createdAt = dto.createdAt;
  }
}
