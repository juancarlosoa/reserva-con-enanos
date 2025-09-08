export interface RoomResponseDTO {
    id: string
    slug: string
    name: string
    capacity: number
    type: string
    hourlyRate: number
    available: boolean
    providerSlug: string
}

export interface RoomRequestDTO {
    name: string
    capacity: number
    type: string
    hourlyRate: number
    available?: boolean
    providerId: string
}


