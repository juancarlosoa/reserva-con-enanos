export interface RoomResponseDTO {
  id: string;
  providerId: string;
  name: string;
  description?: string;
  theme?: string;
  minPlayers: number;
  maxPlayers: number;
  createdAt: string;
  durationMinutes?: number;
}