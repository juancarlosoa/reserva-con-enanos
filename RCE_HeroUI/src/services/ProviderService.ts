import api from "@/api/axiosConfig";
import { REPOSITORY_HOSTS } from "@/api/repositoryHosts";

export const getProviders = async () => {
    const response = await api.get(`${REPOSITORY_HOSTS.PROVIDERS}`);
    return response.data;
};

export const createProvider = async (data: { name: string; email: string; phone: string; }) => {
    const response = await api.post(`${REPOSITORY_HOSTS.PROVIDERS}`, data)
    return response.data;
};