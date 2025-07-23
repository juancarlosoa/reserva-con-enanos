export interface RoomRequestDTO {
  providerId: string;
  name: string;
  description?: string;
  theme?: string;
  minPlayers: number;
  maxPlayers: number;
  durationMinutes?: number;
}
