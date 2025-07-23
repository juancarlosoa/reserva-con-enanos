import { ProviderRequestDTO } from "../dtos/Providers/ProviderRequestDTO";
import { Provider } from "../models/Provider";
import { providerRepository } from "../repositories/ProviderRepository";

export const ProviderService = {

    getProviders: async (): Promise<Provider[]> => {
        const dtos = await providerRepository.getProviders();

        return dtos.map(dto => new Provider(dto));
    },

    createProvider: async (data: ProviderRequestDTO): Promise<Provider> => {
        const dto = await providerRepository.createProvider(data);

        return new Provider(dto);
    },

    deleteProvider: async  (providerId: string): Promise<Provider> => {
        const dto = await providerRepository.deleteProvider(providerId);

        return new Provider(dto);
    },

    updateProvider:  async (providerId: string, data: ProviderRequestDTO): Promise<Provider> => {
        const dto = await providerRepository.updateProvider(providerId, data);

        return new Provider(dto);
    },

    getProviderRooms: async (providerId: string): Promise<Provider> => {
        const dto = await providerRepository.getProviderRooms(providerId);

        return new Provider(dto);
    },

};