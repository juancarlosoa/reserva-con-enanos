import { Provider } from "@/providers/models/Provider";
import { Button, Card, CardHeader, CardBody, CardFooter, Image } from "@heroui/react";
import { useNavigate } from "react-router-dom";
import { ProviderService } from "../services/ProviderService";
import { Icon } from '@iconify-icon/react';

export const ProviderCard = ({ provider }: { provider: Provider }) => {
    const navigate = useNavigate();

    // Eliminar provider con confirmación
    const handleDelete = async (e: React.MouseEvent) => {
        e.stopPropagation();
        if (window.confirm("¿Seguro que deseas eliminar este proveedor?")) {
            await ProviderService.deleteProvider(provider.id);
            window.location.reload();
        }
    };

    const handleEdit = (e: React.MouseEvent) => {
        e.stopPropagation();
        navigate(`/providers/edit/${provider.id}`);
    };

    return (

// https://heroui.com/images/hero-card.jpeg
<Card 
              key={provider.id} 
              className="cursor-pointer hover:shadow-lg transition-shadow duration-200 w-full h-full"
              isPressable
              onPress={() => navigate("/")}
            >
              <CardHeader className="pb-0 p-4">
                <Image
                  src="https://heroui.com/images/hero-card.jpeg"
                  alt={provider.name}
                  className="w-full h-15 object-cover rounded-lg"
                />
              </CardHeader>
              
              <CardBody className="pt-2 px-4">
                <h3 className="text-lg font-semibold text-gray-800 mb-2 truncate">{provider.name}</h3>
                <div className="space-y-1 text-sm text-gray-600">
                  <div className="flex items-center gap-2">
                    <span>{provider.phoneNumber}</span>
                  </div>
                  <div className="flex items-center gap-2">
                    <span className="truncate">{provider.email}</span>
                  </div>
                </div>
              </CardBody>
              
              <CardFooter className="pt-0 px-4 pb-4">
                <div className="flex gap-2 w-full">
                  <Button
                    size="sm"
                    color="primary"
                    variant="flat"
                    className="flex-1 min-w-0"
                  >
                    Editar
                  </Button>
                  <Button
                    size="sm"
                    color="danger"
                    variant="flat"
                    className="flex-1 min-w-0"
                  >
                    Eliminar
                  </Button>
                </div>
              </CardFooter>
            </Card>
    );
};