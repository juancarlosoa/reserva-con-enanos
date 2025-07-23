import {
  Card,
  CardBody,
  CardHeader,
  CardFooter,
  Image,
  Button,
} from "@heroui/react";
import { Room } from "../models/Room";
import { Icon } from "@iconify-icon/react";

interface Props {
  room: Room;
  onEdit: (room: Room) => void;
}

export const RoomCard = ({ room, onEdit }: Props) => {
  // Eliminar provider con confirmación
  const handleDelete = async () => {
    if (window.confirm("¿Seguro que deseas eliminar esta sala?")) {
      // await RoomService.deleteProvider(room.id);
      window.location.reload();
    }
  };

  const handleEdit = () => {
    onEdit(room);
  };
  return (
    <Card key={room.id} className="hover:shadow-xl max-w-xs flex flex-col">
      <CardHeader className="pb-0 p-0">
        <Image
          radius="none"
          src="/public/images/providers.jpg"
          alt={room.name}
          className="w-full h-32 object-cover rounded-t-xl"
        />
      </CardHeader>
      <CardBody className="pt-4 px-6 flex flex-col items-center">
        {/* Info de la sala */}
        <div className="w-full px-6 py-4 flex flex-col items-center">
          <h3 className="text-lg font-bold mb-2 text-center text-blue-700">
            {room.name}
          </h3>
          {room.description && (
            <p className="text-gray-500 mb-2 text-center">{room.description}</p>
          )}
          <div className="flex flex-wrap gap-2 justify-center mb-2">
            {room.theme && (
              <span className="bg-blue-100 text-blue-700 px-2 py-1 rounded text-xs font-medium">
                {room.theme}
              </span>
            )}
            {room.durationMinutes && (
              <span className="bg-gray-100 text-gray-700 px-2 py-1 rounded text-xs font-medium">
                {room.durationMinutes} min
              </span>
            )}
          </div>
          <div className="flex gap-4 text-sm text-gray-600 mb-2">
            <span>
              👥 {room.minPlayers} - {room.maxPlayers} jugadores
            </span>
          </div>
          {room.createdAt && (
            <span className="text-xs text-gray-400">
              Creada: {new Date(room.createdAt).toLocaleDateString()}
            </span>
          )}
        </div>
      </CardBody>
      <CardFooter className="flex gap-2 justify-end px-6 pb-4">
        <Button
          color="primary"
          size="md"
          variant="flat"
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
