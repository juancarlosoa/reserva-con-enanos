import DefaultLayout from "@/layouts/default";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { ProviderService } from "../services/ProviderService";
import { Room } from "../../rooms/models/Room";
import CreateRoomModal from "../../rooms/components/CreateRoomModal";
import { Button, useDisclosure } from "@heroui/react";
import { RoomCard } from "../../rooms/components/RoomCard";

export const ProviderDetail = () => {
  const { name } = useParams();
  const [rooms, setRooms] = useState<Room[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [roomToEdit, setRoomToEdit] = useState<Room | null>(null);
  const { isOpen, onOpen, onOpenChange } = useDisclosure();

  useEffect(() => {
    const fetchRooms = async () => {
      setLoading(true);
      setError(null);
      try {
        // name es el id del provider
        const res = await ProviderService.getProviderRooms(name!);
        // Si la respuesta es un Provider con rooms, ajusta aquí
        setRooms(res.rooms || []);
      } catch (err) {
        setError("No se pudieron cargar las salas");
      } finally {
        setLoading(false);
      }
    };
    if (name) fetchRooms();
  }, [name]);

  const openEditModal = (room: Room) => {
    setRoomToEdit(room);
    onOpen();
  };
  const openCreateModal = () => {
    setRoomToEdit(null);
    onOpen();
  };

  const refreshRooms = async () => {
    try {
      setLoading(true);
      //const data = await ProviderService.getProviders();
      // setRooms(data);
    } catch (error) {
      console.error("Error al refrescar providers:", error);
    } finally {
      setLoading(false);
    }
  };
  return (
    <DefaultLayout>
      <div className="p-6 max-w-6xl mx-auto">
        <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between mb-8 gap-4">
          <div>
            <h2 className="text-3xl font-bold text-blue-700 mb-2">
              Salas del proveedor
            </h2>
            <p className="text-gray-500 text-base">
              ID: <span className="font-mono text-blue-500">{name}</span>
            </p>
          </div>
          <Button
            color="primary"
            variant="solid"
            className="font-semibold px-6 py-2 rounded-lg shadow"
            onPress={openCreateModal}
          >
            Crear sala
          </Button>
        </div>
        {loading ? (
          <div className="text-center py-10 text-lg text-gray-400">
            Cargando salas...
          </div>
        ) : error ? (
          <div className="text-center py-10 text-red-500">{error}</div>
        ) : rooms.length === 0 ? (
          <div className="text-center py-10 text-gray-400">
            No hay salas para este proveedor.
          </div>
        ) : (
          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-8">
            {rooms.map((room) => (
              <RoomCard key={room.id} room={room} onEdit={openEditModal} />
            ))}
          </div>
        )}
        <CreateRoomModal
          isOpen={isOpen}
          onOpenChange={onOpenChange}
          onFinish={refreshRooms}
          roomToEdit={roomToEdit}
        />
      </div>
    </DefaultLayout>
  );
};
