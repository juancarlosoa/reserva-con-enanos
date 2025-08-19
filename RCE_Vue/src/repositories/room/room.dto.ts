export interface RoomResponseDTO {
    id: string
    name: string
    capacity: number
    type: string
    hourlyRate: number
    available: boolean
    providerId: string
}

export interface RoomRequestDTO {
    name: string
    capacity: number
    type: string
    hourlyRate: number
    available?: boolean
    providerId: string
}


