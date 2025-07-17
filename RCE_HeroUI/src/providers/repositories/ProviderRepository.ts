import { CreateProviderDTO } from "@/providers/dtos/Providers/CreateProviderDTO";
import { REPOSITORY_HOSTS } from "../../api/repositoryHosts";
import { apiAdapter } from "@/api";
import { ProviderResponseDTO } from "../dtos/Providers/ProviderResponseDTO";
import { UpdateProviderDTO } from "../dtos/Providers/UpdateProviderDTO";

export const providerRepository = {

    getProviders: async () => {
        return apiAdapter.get<ProviderResponseDTO[]>(`${REPOSITORY_HOSTS.PROVIDERS}`);
    },

    createProvider: async (data: CreateProviderDTO) => {
        return apiAdapter.post<ProviderResponseDTO>(`${REPOSITORY_HOSTS.PROVIDERS}`, data)
    },

    deleteProvider: async (providerId: string) => {
        return apiAdapter.delete(`${REPOSITORY_HOSTS.PROVIDERS}/${providerId}`);
    },

    updateProvider: async (data: UpdateProviderDTO) => {
        return apiAdapter.post<ProviderResponseDTO>(`${REPOSITORY_HOSTS.PROVIDERS}`, data);
    },

    getProviderRooms: async (providerId: string) => {
        return apiAdapter.get(`${REPOSITORY_HOSTS.PROVIDERS}/${providerId}/rooms`);
    }
};