import {
  Modal,
  ModalContent,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Button,
  Input,
  Form,
} from "@heroui/react";
import { useState } from "react";
import { Room } from "../models/Room";

interface Props {
  isOpen: boolean;
  onOpenChange: (open: boolean) => void;
  onFinish: (room: Room) => void;
  providerId: string;
}

export default function CreateRoomModal({
  isOpen,
  onOpenChange,
  onFinish,
  providerId,
}: Props) {
  const [formData, setFormData] = useState({
    name: "",
    description: "",
    capacity: "",
    imageUrl: "",
  });
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [formErrors, setFormErrors] = useState({
    name: "",
    capacity: "",
  });

  const handleChange = (field: string, value: string) => {
    setFormData((prev) => ({ ...prev, [field]: value }));
    setFormErrors((prev) => ({ ...prev, [field]: "" }));
  };

  const validateForm = () => {
    const errors: any = {};
    if (!formData.name.trim()) errors.name = "El nombre es obligatorio.";
    if (formData.capacity && isNaN(Number(formData.capacity)))
      errors.capacity = "La capacidad debe ser un número.";
    setFormErrors(errors);
    return Object.keys(errors).length === 0;
  };

  const onSubmit = async (onClose: () => void) => {
    if (!validateForm()) {
      setError("Corrige los errores antes de continuar.");
      return;
    }
    setIsSubmitting(true);
    setError(null);
    try {
      // Aquí iría la llamada a la API para crear la sala
      // Ejemplo: await RoomService.createRoom(providerId, formData)
      // Simulación:
      const newRoom: Room = {
        id: Math.random().toString(36).substring(2),
        name: formData.name,
        description: formData.description,
        capacity: Number(formData.capacity),
        imageUrl: formData.imageUrl,
      };
      onFinish(newRoom);
      onClose();
      setFormData({ name: "", description: "", capacity: "", imageUrl: "" });
    } catch (err) {
      setError("Hubo un error al crear la sala.");
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <Modal
      isOpen={isOpen}
      placement="center"
      onOpenChange={onOpenChange}
      backdrop="blur"
    >
      <ModalContent>
        {(onClose) => (
          <>
            <ModalHeader className="flex flex-col gap-1">
              Crear nueva sala
            </ModalHeader>
            <ModalBody>
              <Form>
                <Input
                  label="Nombre"
                  placeholder="Nombre de la sala"
                  variant="bordered"
                  value={formData.name}
                  isRequired
                  onValueChange={(val) => handleChange("name", val)}
                  errorMessage={formErrors.name}
                />
                <Input
                  label="Descripción"
                  placeholder="Descripción"
                  variant="bordered"
                  value={formData.description}
                  onValueChange={(val) => handleChange("description", val)}
                />
                <Input
                  label="Capacidad"
                  placeholder="Capacidad"
                  type="number"
                  variant="bordered"
                  value={formData.capacity}
                  onValueChange={(val) => handleChange("capacity", val)}
                  errorMessage={formErrors.capacity}
                />
                <Input
                  label="Imagen (URL)"
                  placeholder="URL de la imagen"
                  variant="bordered"
                  value={formData.imageUrl}
                  onValueChange={(val) => handleChange("imageUrl", val)}
                />
                {error && <p className="text-red-500 text-sm mt-2">{error}</p>}
              </Form>
            </ModalBody>
            <ModalFooter>
              <Button color="danger" variant="flat" onPress={onClose}>
                Cancelar
              </Button>
              <Button
                color="primary"
                isLoading={isSubmitting}
                onPress={() => onSubmit(onClose)}
              >
                Crear
              </Button>
            </ModalFooter>
          </>
        )}
      </ModalContent>
    </Modal>
  );
}
