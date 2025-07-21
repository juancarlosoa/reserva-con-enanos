import DefaultLayout from "@/layouts/default";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { ProviderService } from "../services/ProviderService";
import { Room } from "../../rooms/models/Room";
import CreateRoomModal from "../../rooms/components/CreateRoomModal";
import { Button } from "@heroui/react";
import { Card } from "@heroui/react";

export const ProviderDetail = () => {
  const { name } = useParams();
  const [rooms, setRooms] = useState<Room[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [isModalOpen, setIsModalOpen] = useState(false);

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
            onPress={() => setIsModalOpen(true)}
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
              <Card
                key={room.id}
                className="p-0 flex flex-col items-center shadow-lg border border-gray-200 hover:shadow-xl transition-shadow bg-white rounded-xl overflow-hidden"
              >
                {/* Imagen superior */}
                <div className="w-full">
                  {room.imageUrl ? (
                    <img
                      src={room.imageUrl}
                      alt={room.name}
                      className="w-full h-40 object-cover rounded-t-xl"
                    />
                  ) : (
                    <div className="w-full h-40 bg-gradient-to-br from-blue-100 to-gray-100 rounded-t-xl flex items-center justify-center text-blue-400 text-2xl">
                      <span>🏠</span>
                    </div>
                  )}
                </div>
                {/* Info de la sala */}
                <div className="w-full px-6 py-4 flex flex-col items-center">
                  <h3 className="text-lg font-bold mb-2 text-center text-blue-700">
                    {room.name}
                  </h3>
                  {room.description && (
                    <p className="text-gray-500 mb-2 text-center">
                      {room.description}
                    </p>
                  )}
                  {room.capacity && (
                    <span className="text-sm text-blue-500 font-medium">
                      Capacidad: {room.capacity}
                    </span>
                  )}
                </div>
              </Card>
            ))}
          </div>
        )}
        <CreateRoomModal
          isOpen={isModalOpen}
          onOpenChange={setIsModalOpen}
          onFinish={(newRoom) => setRooms((prev) => [...prev, newRoom])}
          providerId={name || ""}
        />
      </div>
    </DefaultLayout>
  );
};
