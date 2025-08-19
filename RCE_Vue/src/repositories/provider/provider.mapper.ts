import type { Provider } from '@/models/Provider'
import type { ProviderRequestDTO, ProviderResponseDTO } from './provider.dto'

export function mapProviderResponseToModel(dto: ProviderResponseDTO): Provider {
    return {
        id: dto.id,
        name: dto.name,
        email: dto.email,
        phone: dto.phone,
        address: dto.address,
        logo: dto.logo,
        active: dto.active,
        roomCount: dto.roomCount,
        bookingsThisMonth: dto.bookingsThisMonth
    }
}

export function mapProviderModelToRequest(model: Provider): ProviderRequestDTO {
    return {
        name: model.name,
        email: model.email,
        phone: model.phone,
        address: model.address,
        logo: model.logo,
        active: model.active
    }
}

