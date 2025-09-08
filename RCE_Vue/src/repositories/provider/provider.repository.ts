import { apiAdapter } from '@/core/http/apiAdapter'
import { REPOSITORY_HOSTS } from '@/core/constants/repositoryHosts'
import type { Provider } from '@/models/Provider'
import type { Room } from '@/models/Room'
import type { ProviderRequestDTO, ProviderResponseDTO } from './provider.dto'
import { mapProviderResponseToModel, mapProviderModelToRequest } from './provider.mapper'

export const providerRepository = {
    async getProviders(): Promise<Provider[]> {
        const dtos = await apiAdapter.get<ProviderResponseDTO[]>(`${REPOSITORY_HOSTS.PROVIDERS}`)
        return dtos.map(mapProviderResponseToModel)
    },

    async getProviderById(providerId: string): Promise<Provider> {
        const dto = await apiAdapter.get<ProviderResponseDTO>(`${REPOSITORY_HOSTS.PROVIDERS}/${providerId}`)
        return mapProviderResponseToModel(dto)
    },

    async createProvider(model: Provider): Promise<Provider> {
        const payload: ProviderRequestDTO = mapProviderModelToRequest(model)
        const dto = await apiAdapter.post<ProviderResponseDTO, ProviderRequestDTO>(`${REPOSITORY_HOSTS.PROVIDERS}`, payload)
        return mapProviderResponseToModel(dto)
    },

    async deleteProvider(providerId: string): Promise<void> {
        await apiAdapter.delete(`${REPOSITORY_HOSTS.PROVIDERS}/${providerId}`)
    },

    async updateProvider(providerId: string, model: Provider): Promise<Provider> {
        const payload: ProviderRequestDTO = mapProviderModelToRequest(model)
        const dto = await apiAdapter.put<ProviderResponseDTO, ProviderRequestDTO>(`${REPOSITORY_HOSTS.PROVIDERS}/${providerId}`, payload)
        return mapProviderResponseToModel(dto)
    },

    async getProviderRooms(providerSlug: string): Promise<Room[]> {
        return await apiAdapter.get<Room[]>(`${REPOSITORY_HOSTS.PROVIDERS}/${providerSlug}/rooms`)
    }
}

