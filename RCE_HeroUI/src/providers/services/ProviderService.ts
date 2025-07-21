import { CreateProviderDTO } from "../dtos/Providers/CreateProviderDTO";
import { UpdateProviderDTO } from "../dtos/Providers/UpdateProviderDTO";
import { Provider } from "../models/Provider";
import { providerRepository } from "../repositories/ProviderRepository";

export const ProviderService = {

    getProviders: async (): Promise<Provider[]> => {
        const dtos = await providerRepository.getProviders();

        return dtos.map(dto => new Provider(dto));
    },

    createProvider: async (data: CreateProviderDTO): Promise<Provider> => {
        const dto = await providerRepository.createProvider(data);

        return new Provider(dto);
    },

    deleteProvider: async  (providerId: string): Promise<Provider> => {
        const dto = await providerRepository.deleteProvider(providerId);

        return new Provider(dto);
    },

    updateProvider:  async (providerId: string, data: UpdateProviderDTO): Promise<Provider> => {
        const dto = await providerRepository.updateProvider(providerId, data);

        return new Provider(dto);
    },

    getProviderRooms: async (providerId: string): Promise<Provider> => {
        const dto = await providerRepository.getProviderRooms(providerId);

        return new Provider(dto);
    },

};