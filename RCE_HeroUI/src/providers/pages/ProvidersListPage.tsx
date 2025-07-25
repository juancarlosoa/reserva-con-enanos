import { ProviderCard } from "@/providers/components/ProviderCard";
import DefaultLayout from "@/layouts/default";
import { useEffect, useState } from "react";
import { useDisclosure, Button } from "@heroui/react";
import CreateEditProviderModal from "@/providers/components/CreateEditProviderModal";
import { Provider } from "@/providers/models/Provider";
import { ProviderService } from "../services/ProviderService";
import { Icon } from "@iconify-icon/react";
import { useNavigate } from "react-router-dom";

export const ProvidersList = () => {
  const [providers, setProviders] = useState<Provider[]>([]);
  const [loading, setLoading] = useState(true);
  const [providerToEdit, setProviderToEdit] = useState<Provider | null>(null);
  const { isOpen, onOpen, onOpenChange } = useDisclosure();
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    setLoading(true);
    setError(null);
    ProviderService.getProviders()
      .then(setProviders)
      .catch((err) => {
        setError("No se pudieron cargar los proveedores");
        console.error(err);
      })
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

  return (
    <main>
      <div className="max-w-6xl mx-auto p-4 sm:p-6">
        <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between mb-8 gap-4">
          <h1 className="text-3xl font-bold text-green-800">Proveedores</h1>
          <Button
            color="success"
            variant="solid"
            className="font-semibold px-6 py-2 rounded-lg shadow"
            onPress={openCreateModal}
          >
            <Icon
              icon="heroicons:plus-circle"
              width="24"
              height="24"
              className="mr-2"
            />
            Crear nuevo proveedor
          </Button>
        </div>
        {/* Estados visuales */}
        {loading ? (
          <div className="flex flex-col items-center justify-center py-16 text-lg text-green-400">
            <Icon
              icon="heroicons:arrow-path"
              width="40"
              height="40"
              className="animate-spin mb-2"
            />
            Cargando proveedores...
          </div>
        ) : error ? (
          <div className="flex flex-col items-center justify-center py-16">
            <Icon
              icon="heroicons:exclamation-triangle"
              width="40"
              height="40"
              className="mb-2"
            />
            {error}
          </div>
        ) : providers.length === 0 ? (
          <div className="flex flex-col items-center justify-center py-16 text-lg text-green-400">
            <Icon
              icon="heroicons:user-group"
              width="40"
              height="40"
              className="mb-2"
            />
            No hay proveedores registrados.
          </div>
        ) : (
          <div className="flex gap-8 mt-8">
            {providers.map((provider) => (
              <div
                key={provider.id}
                className="cursor-pointer transition-transform hover:scale-105"
              >
                <ProviderCard provider={provider} onEdit={openEditModal} />
              </div>
            ))}
          </div>
        )}
        <CreateEditProviderModal
          isOpen={isOpen}
          onOpenChange={onOpenChange}
          onFinish={refreshProviders}
          providerToEdit={providerToEdit}
        />
      </div>
    </main>
  );
};
