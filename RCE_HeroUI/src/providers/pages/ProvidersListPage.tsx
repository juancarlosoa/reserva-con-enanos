import { ProviderCard } from "@/providers/components/ProviderCard";
import DefaultLayout from "@/layouts/default";
import { useEffect, useState } from "react";
import { useDisclosure, Button } from "@heroui/react";
import CreateProviderModal from "@/providers/components/CreateProviderModal";
import { Provider } from "@/providers/models/Provider";
import { ProviderService } from "../services/ProviderService";

export const ProvidersList = () => {
    const [providers, setProviders] = useState<Provider[]>([]);
    const [loading, setLoading] = useState(true);
    const { isOpen, onOpen, onOpenChange } = useDisclosure();

    useEffect(() => {
        ProviderService.getProviders()
            .then(setProviders)
            .catch(console.error)
            .finally(() => setLoading(false));
    }, []);


    const refreshProviders = async () => {
        try {
            setLoading(true);
            const data = await ProviderService.getProviders();
            setProviders(data);
        } catch (error) {
            console.error("Error al refrescar providers:", error);
        } finally {
            setLoading(false);
        }
    };

    if (loading) return <p className="text-center mt-8">Loading...</p>;

    return (
        <DefaultLayout> 
            <div className="flex justify-between items-center mb-8">
                <h1 className="text-3xl font-bold text-gray-800">Providers</h1>
                <Button color="primary" onPress={onOpen}>
                    + Crear nuevo proveedor
                </Button>
            </div>
              <div className="flex gap-4">
                {providers.map(provider => (
                    <div className="max-w-xs">
                        <ProviderCard key={provider.id} provider={provider} />
                    </div>
                ))}
            </div>
            <CreateProviderModal isOpen={isOpen} onOpenChange={onOpenChange} onCreate={refreshProviders} />
        </DefaultLayout>
    );
};