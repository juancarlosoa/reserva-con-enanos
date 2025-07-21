import { ProviderCard } from "@/providers/components/ProviderCard";
import DefaultLayout from "@/layouts/default";
import { useEffect, useState } from "react";
import { useDisclosure, Button } from "@heroui/react";
import CreateEditProviderModal from "@/providers/components/CreateEditProviderModal";
import { Provider } from "@/providers/models/Provider";
import { ProviderService } from "../services/ProviderService";

export const ProvidersList = () => {
  const [providers, setProviders] = useState<Provider[]>([]);
  const [loading, setLoading] = useState(true);
  const [providerToEdit, setProviderToEdit] = useState<Provider | null>(null);
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

  const openCreateModal = () => {
    setProviderToEdit(null);
    onOpen();
  };

  const openEditModal = (provider: Provider) => {
    setProviderToEdit(provider);
    onOpen();
  };

  if (loading) return <p className="text-center mt-8">Loading...</p>;

  return (
    <DefaultLayout>
      <div className="max-w-6xl mx-auto p-6">
        <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between mb-8 gap-4">
          <div>
            <h1 className="text-3xl font-bold text-blue-700 mb-2">
              Proveedores
            </h1>
          </div>
          <Button
            color="primary"
            variant="solid"
            className="font-semibold px-6 py-2 rounded-lg shadow"
            onPress={openCreateModal}
          >
            + Crear nuevo proveedor
          </Button>
        </div>
        <div className="flex gap-4 mt-8">
          {providers.map((provider) => (
            <ProviderCard
              key={provider.id}
              provider={provider}
              onEdit={openEditModal}
            />
          ))}
        </div>
        <CreateEditProviderModal
          isOpen={isOpen}
          onOpenChange={onOpenChange}
          onFinish={refreshProviders}
          providerToEdit={providerToEdit}
        />
      </div>
    </DefaultLayout>
  );
};
