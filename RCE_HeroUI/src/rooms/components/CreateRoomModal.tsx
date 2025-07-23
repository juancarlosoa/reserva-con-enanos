import {
  Modal,
  ModalContent,
  ModalHeader,
  ModalBody,
  Button,
  Input,
  Form,
} from "@heroui/react";
import { useState } from "react";
import { Room } from "../models/Room";
import { RoomRequestDTO } from "../dtos/RoomRequestDTO";

interface Props {
  isOpen: boolean;
  onOpenChange: (open: boolean) => void;
  onFinish: () => void;
  roomToEdit: Room | null;
}

export default function CreateRoomModal({
  isOpen,
  onOpenChange,
  onFinish,
  roomToEdit,
}: Props) {
  const [formData, setFormData] = useState({
    name: "",
    description: "",
    theme: "",
    minPlayers: "",
    maxPlayers: "",
    durationMinutes: "",
  });
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleChange = (field: string, value: string) => {
    setFormData((prev) => ({ ...prev, [field]: value }));
  };
  const handleCreate = async (onClose: () => void) => {
    setIsSubmitting(true);
    try {
      const dto: RoomRequestDTO = {
        providerId: "",
        name: formData.name,
        description: formData.description,
        theme: formData.theme,
        minPlayers: Number(formData.minPlayers),
        maxPlayers: Number(formData.maxPlayers),
        durationMinutes: Number(formData.durationMinutes),
      };
      // await ProviderService.createProvider(dto);
      onClose();
      setFormData({
        name: "",
        description: "",
        theme: "",
        minPlayers: "",
        maxPlayers: "",
        durationMinutes: "",
      });
      onFinish();
    } catch (err) {
      console.error(err);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleEdit = async (onClose: () => void) => {
    setIsSubmitting(true);
    try {
      if (roomToEdit?.id) {
        const dto: RoomRequestDTO = {
          providerId: "",
          name: formData.name,
          description: formData.description,
          theme: formData.theme,
          minPlayers: Number(formData.minPlayers),
          maxPlayers: Number(formData.maxPlayers),
          durationMinutes: Number(formData.durationMinutes),
        };
        // await ProviderService.updateProvider(providerToEdit.id, dto);
        onClose();
        setFormData({
          name: "",
          description: "",
          theme: "",
          minPlayers: "",
          maxPlayers: "",
          durationMinutes: "",
        });
        onFinish();
      }
    } catch (err) {
      console.error(err);
    } finally {
      setIsSubmitting(false);
    }
  };

  const onSubmit = async (onClose: () => void) => {
    if (roomToEdit) {
      await handleEdit(onClose);
    } else {
      await handleCreate(onClose);
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
                />
                <Input
                  label="Descripción"
                  placeholder="Descripción"
                  variant="bordered"
                  value={formData.description}
                  onValueChange={(val) => handleChange("description", val)}
                />
                <Input
                  label="Temática"
                  placeholder="Temática de la sala"
                  variant="bordered"
                  value={formData.theme}
                  onValueChange={(val) => handleChange("theme", val)}
                />
                <div className="flex gap-4">
                  <Input
                    label="Mín. jugadores"
                    placeholder="Mínimo"
                    type="number"
                    variant="bordered"
                    value={formData.minPlayers}
                    min={0}
                    onValueChange={(val) => handleChange("minPlayers", val)}
                  />
                  <Input
                    label="Máx. jugadores"
                    placeholder="Máximo"
                    type="number"
                    variant="bordered"
                    value={formData.maxPlayers}
                    onValueChange={(val) => handleChange("maxPlayers", val)}
                  />
                </div>
                <Input
                  label="Duración (min)"
                  placeholder="Duración en minutos"
                  type="number"
                  variant="bordered"
                  value={formData.durationMinutes}
                  min={0}
                  onValueChange={(val) => handleChange("durationMinutes", val)}
                />
                {/* Campo de imagen eliminado porque Room no tiene imageUrl */}
                <div className="flex gap-2 mt-4 justify-end">
                  <Button
                    color="danger"
                    variant="flat"
                    type="reset"
                    onPress={onClose}
                  >
                    Cancelar
                  </Button>
                  <Button
                    type="submit"
                    color="primary"
                    isLoading={isSubmitting}
                    onPress={() => onSubmit(onClose)}
                  >
                    Crear
                  </Button>
                </div>
              </Form>
            </ModalBody>
          </>
        )}
      </ModalContent>
    </Modal>
  );
}
