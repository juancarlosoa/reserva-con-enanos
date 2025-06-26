import { ProviderCard } from "@/components/Providers/ProviderCard";
import DefaultLayout from "@/layouts/default";
import { getProviders } from "@/services/ProviderService";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useDisclosure, Button } from "@heroui/react";
import CreateProviderModal from "@/components/Providers/CreateProviderModal";

type Provider = {
    id: string;
    name: string;
    description: string;
    imageUrl: string;
    createdAt: string;
};

export const ProvidersList = () => {
    const [providers, setProviders] = useState<Provider[]>([]);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();
    const { isOpen, onOpen, onOpenChange } = useDisclosure();

    useEffect(() => {
        getProviders()
            .then(data => setProviders(data))
            .catch(console.error)
            .finally(() => setLoading(false));
    }, []);

    if (loading) return <p className="text-center mt-8">Cargando...</p>;

    return (
        <DefaultLayout>
            <div className="flex justify-between items-center mb-8">
                <h1 className="text-3xl font-bold text-gray-800">Providers</h1>
                <Button color="primary" onPress={onOpen}>
                    Crear nuevo proveedor
                </Button>

                {/* Modal separado */}
                <CreateProviderModal isOpen={isOpen} onOpenChange={onOpenChange} />
            </div>
            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 p-6">
                {providers.map(provider => (
                    <ProviderCard key={provider.id} provider={provider} />
                ))}
            </div>
        </DefaultLayout>
    );
};