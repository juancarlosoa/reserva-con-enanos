import { ProviderRequestDTO } from "@/providers/dtos/Providers/ProviderRequestDTO";
import { REPOSITORY_HOSTS } from "../../api/repositoryHosts";
import { apiAdapter } from "@/api";
import { ProviderResponseDTO } from "../dtos/Providers/ProviderResponseDTO";

export const providerRepository = {

    getProviders: async () => {
        return apiAdapter.get<ProviderResponseDTO[]>(`${REPOSITORY_HOSTS.PROVIDERS}`);
    },

    createProvider: async (data: ProviderRequestDTO) => {
        return apiAdapter.post<ProviderResponseDTO>(`${REPOSITORY_HOSTS.PROVIDERS}`, data)
    },

    deleteProvider: async (providerId: string) => {
        return apiAdapter.delete(`${REPOSITORY_HOSTS.PROVIDERS}/${providerId}`);
    },

    updateProvider: async (providerId: string, data: ProviderRequestDTO) => {
        return apiAdapter.put<ProviderResponseDTO>(`${REPOSITORY_HOSTS.PROVIDERS}/${providerId}`, data);
    },

    getProviderRooms: async (providerId: string) => {
        return apiAdapter.get(`${REPOSITORY_HOSTS.PROVIDERS}/${providerId}/rooms`);
    }
};