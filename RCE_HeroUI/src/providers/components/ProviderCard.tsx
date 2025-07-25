import { Provider } from "@/providers/models/Provider";
import {
  Button,
  Card,
  CardHeader,
  CardBody,
  CardFooter,
  Image,
} from "@heroui/react";
import { useNavigate } from "react-router-dom";
import { ProviderService } from "../services/ProviderService";
import { Icon } from "@iconify-icon/react";

interface Props {
  provider: Provider;
  onEdit: (provider: Provider) => void;
}

export const ProviderCard = ({ provider, onEdit }: Props) => {
  const navigate = useNavigate();

  // Eliminar provider con confirmación
  const handleDelete = async () => {
    if (window.confirm("¿Seguro que deseas eliminar este proveedor?")) {
      await ProviderService.deleteProvider(provider.id);
      window.location.reload();
    }
  };

  const handleEdit = () => {
    onEdit(provider);
  };
  return (
    <Card key={provider.id} className="hover:shadow-xl max-w-xs flex flex-col">
      <CardHeader className="pb-0 p-0">
        <Image
          radius="none"
          src="/public/images/providers.jpg"
          alt={provider.name}
          onClick={() => navigate(`/providers/${provider.id}`)}
          className="cursor-pointer w-full h-32 object-cover rounded-t-xl"
        />
      </CardHeader>
      <CardBody className="pt-4 px-6 flex flex-col items-center">
        <h3 className="text-lg font-bold text-green-700 mb-2 truncate">
          {provider.name}
        </h3>
        <div className="space-y-1 text-sm text-gray-600 w-full">
          <div className="flex items-center gap-2 mb-2">
            <Icon icon="heroicons:device-phone-mobile" width="20" height="20" />
            <span>{provider.phoneNumber}</span>
          </div>
          <div className="flex items-center gap-2">
            <Icon icon="heroicons:envelope" width="20" height="20" />
            <span className="truncate">{provider.email}</span>
          </div>
        </div>
      </CardBody>
      <CardFooter className="flex gap-2 justify-end px-6 pb-4">
        <Button
          color="primary"
          size="md"
          variant="ghost"
          className="px-6 py-3 text-base"
          onPress={handleEdit}
        >
          <Icon icon="heroicons:pencil-square" width="24" height="24" /> Editar
        </Button>
        <Button
          color="danger"
          size="md"
          variant="flat"
          className="px-6 py-3 text-base"
          onPress={handleDelete}
        >
          <Icon icon="heroicons:trash" width="24" height="24" /> Eliminar
        </Button>
      </CardFooter>
    </Card>
  );
};
