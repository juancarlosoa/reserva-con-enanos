import {
  Modal,
  ModalContent,
  ModalHeader,
  ModalBody,
  Button,
  Input,
  Form,
} from "@heroui/react";
import { useEffect, useState } from "react";
import { ProviderService } from "../services/ProviderService";
import { CreateProviderDTO } from "../dtos/Providers/CreateProviderDTO";
import { Provider } from "../models/Provider";
import { UpdateProviderDTO } from "../dtos/Providers/UpdateProviderDTO";

interface Props {
  isOpen: boolean;
  onOpenChange: (open: boolean) => void;
  onFinish: () => void;
  providerToEdit: Provider | null;
}

export default function CreateEditProviderModal({
  isOpen,
  onOpenChange,
  onFinish,
  providerToEdit,
}: Props) {
  const [formData, setFormData] = useState({
    name: "",
    email: "",
    phoneNumber: "",
  });

  useEffect(() => {
    if (providerToEdit) {
      setFormData({
        name: providerToEdit.name,
        email: providerToEdit.email,
        phoneNumber: providerToEdit.phoneNumber,
      });
    } else {
      setFormData({ name: "", email: "", phoneNumber: "" });
    }
  }, [providerToEdit]);

  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleChange = (field: string, value: string) => {
    setFormData((prev) => ({ ...prev, [field]: value }));
  };

  const handleCreate = async (onClose: () => void) => {
    setIsSubmitting(true);
    try {
      const dto: CreateProviderDTO = {
        name: formData.name,
        email: formData.email,
        phoneNumber: formData.phoneNumber,
      };
      await ProviderService.createProvider(dto);
      onClose();
      setFormData({ name: "", email: "", phoneNumber: "" });
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
      if (providerToEdit?.id) {
        const dto: UpdateProviderDTO = {
          name: formData.name,
          email: formData.email,
          phoneNumber: formData.phoneNumber,
        };
        await ProviderService.updateProvider(providerToEdit.id, dto);
        onClose();
        setFormData({ name: "", email: "", phoneNumber: "" });
        onFinish();
      }
    } catch (err) {
      console.error(err);
    } finally {
      setIsSubmitting(false);
    }
  };

  const onSubmit = async (onClose: () => void) => {
    if (providerToEdit) {
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
          <div className="bg-gradient-to-br from-blue-50 to-white rounded-xl p-6 w-full max-w-md mx-auto">
            <ModalHeader className="flex flex-col gap-2 items-center mb-2">
              <span className="text-4xl text-blue-600">
                {providerToEdit ? "✏️" : "➕"}
              </span>
              <span className="text-2xl font-bold text-blue-700">
                {providerToEdit ? "Editar proveedor" : "Crear nuevo proveedor"}
              </span>
            </ModalHeader>
            <ModalBody>
              <Form
                className="w-full flex flex-col gap-4"
                onReset={() => onClose()}
                onSubmit={(e) => {
                  e.preventDefault();
                  onSubmit(onClose);
                }}
              >
                <Input
                  label="Nombre"
                  placeholder="Nombre del proveedor"
                  variant="bordered"
                  value={formData.name}
                  autoFocus
                  isRequired
                  onValueChange={(val) => handleChange("name", val)}
                />
                <Input
                  label="Email"
                  placeholder="Email del proveedor"
                  type="email"
                  variant="bordered"
                  value={formData.email}
                  isRequired
                  onValueChange={(val) => handleChange("email", val)}
                />
                <Input
                  label="Teléfono"
                  placeholder="Teléfono de contacto"
                  type="tel"
                  variant="bordered"
                  value={formData.phoneNumber}
                  isRequired
                  onValueChange={(val) => handleChange("phoneNumber", val)}
                />
                <div className="flex gap-2 justify-end mt-4">
                  <Button color="danger" variant="flat" type="reset">
                    Cancelar
                  </Button>
                  <Button
                    color="primary"
                    type="submit"
                    isLoading={isSubmitting}
                  >
                    {providerToEdit ? "Guardar cambios" : "Crear proveedor"}
                  </Button>
                </div>
              </Form>
            </ModalBody>
          </div>
        )}
      </ModalContent>
    </Modal>
  );
}
