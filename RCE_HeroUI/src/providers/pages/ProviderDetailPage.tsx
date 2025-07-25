import { useParams, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { ProviderService } from "../services/ProviderService";
import { Room } from "../../rooms/models/Room";
import CreateEditRoomModal from "../../rooms/components/CreateEditRoomModal";
import { Button, useDisclosure } from "@heroui/react";
import { RoomCard } from "../../rooms/components/RoomCard";
import { Icon } from "@iconify-icon/react";

export const ProviderDetail = () => {
  const { id } = useParams();
  const navigate = useNavigate();
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
        const res = await ProviderService.getProviderById(id!);
        setRooms(res.rooms || []);
        console.log(res.rooms);
      } catch (err) {
        setError("No se pudieron cargar las salas");
      } finally {
        setLoading(false);
      }
    };
    if (id) fetchRooms();
  }, [id]);

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
      const data = await ProviderService.getProviderRooms(id!);
      setRooms(data);
    } catch (error) {
      console.error("Error al refrescar providers:", error);
    } finally {
      setLoading(false);
    }
  };
  return (
    <main>
      <div className="max-w-6xl mx-auto p-4 sm:p-6">
        <div className="mb-6 flex items-center gap-2">
          <Button
            variant="flat"
            color="success"
            className="flex items-center gap-2 px-4 py-2"
            onPress={() => navigate("/providers")}
          >
            <Icon icon="heroicons:arrow-left" width="20" height="20" />
            Volver a proveedores
          </Button>
        </div>
        <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between mb-8 gap-4">
          <div>
            <h1 className="text-3xl font-bold text-green-700 mb-2">
              Salas de la empresa
            </h1>
          </div>
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
              className="mr-2 text-green-700"
            />
            Crear nueva sala
          </Button>
        </div>
        {loading ? (
          <div className="flex flex-col items-center justify-center py-16 text-lg text-green-400">
            <Icon
              icon="heroicons:arrow-path"
              width="40"
              height="40"
              className="animate-spin mb-2 text-green-500"
            />
            Cargando salas...
          </div>
        ) : error ? (
          <div className="flex flex-col items-center justify-center py-16">
            <Icon
              icon="heroicons:exclamation-triangle"
              width="40"
              height="40"
              className="mb-2 text-green-500"
            />
            {error}
          </div>
        ) : rooms.length === 0 ? (
          <div className="flex flex-col items-center justify-center py-16 text-lg text-green-400">
            <Icon
              icon="heroicons:home-modern"
              width="40"
              height="40"
              className="mb-2 text-green-500"
            />
            No hay salas para este proveedor.
          </div>
        ) : (
          <div className="flex gap-8 mt-8">
            {rooms.map((room) => (
              <RoomCard key={room.id} room={room} onEdit={openEditModal} />
            ))}
          </div>
        )}
        <CreateEditRoomModal
          isOpen={isOpen}
          onOpenChange={onOpenChange}
          onFinish={refreshRooms}
          roomToEdit={roomToEdit}
          providerId={id!}
        />
      </div>
    </main>
  );
};
