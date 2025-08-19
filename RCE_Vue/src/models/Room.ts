export interface Room {
  id: string
  name: string
  capacity: number
  type: string
  hourlyRate: number
  available: boolean
  providerId: string
}