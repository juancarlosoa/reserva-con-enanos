import { ProviderResponseDTO } from "../dtos/Providers/ProviderResponseDTO";

export class Provider {
    id: string;
    name: string;
    email: string;
    phoneNumber: string;
    createdAt: string;

    constructor(dto: ProviderResponseDTO) {
        this.id = dto.id;
        this.name = dto.name;
        this.email = dto.email;
        this.phoneNumber = dto.phoneNumber;
        this.createdAt = dto.createdAt;
    }
}