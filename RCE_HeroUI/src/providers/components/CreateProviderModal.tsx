import {
    Modal,
    ModalContent,
    ModalHeader,
    ModalBody,
    ModalFooter,
    Button,
    Input,
} from "@heroui/react";
import { useState } from "react";
import { ProviderService } from "../services/ProviderService";
import { CreateProviderDTO } from "../dtos/Providers/CreateProviderDTO";

interface Props {
    isOpen: boolean;
    onOpenChange: (open: boolean) => void;
    onCreate: () => void;
}

export default function CreateProviderModal({ isOpen, onOpenChange, onCreate }: Props) {
    const [formData, setFormData] = useState({
        name: "",
        email: "",
        phoneNumber: "",
    });

    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const handleChange = (field: string, value: string) => {
        setFormData((prev) => ({ ...prev, [field]: value }));
    };

    const handleCreate = async (onClose: () => void) => {
        setIsSubmitting(true);
        setError(null);

        try {
            const dto: CreateProviderDTO = {
                name: formData.name,
                email: formData.email,
                phoneNumber: formData.phoneNumber
            };
            await ProviderService.createProvider(dto);
            onClose();
            setFormData({ name: "", email: "", phoneNumber: "" });
            onCreate();
        } catch (err) {
            console.error(err);
            setError("Hubo un error al crear el proveedor.");
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <Modal isOpen={isOpen} placement="center" onOpenChange={onOpenChange} backdrop="blur">
            <ModalContent>
                {(onClose) => (
                    <>
                        <ModalHeader className="flex flex-col gap-1">Create Provider</ModalHeader>
                        <ModalBody>
                            <Input
                                label="Name"
                                placeholder="Enter name"
                                variant="bordered"
                                autoFocus
                                onValueChange={(val) => handleChange("name", val)}
                            />
                            <Input
                                label="Email"
                                placeholder="Enter email"
                                type="email"
                                variant="bordered"
                               
                                onValueChange={(val) => handleChange("email", val)}
                            />
                            <Input
                                label="Phone number"
                                placeholder="Enter phone number"
                                type="tel"
                                variant="bordered"
                                value={formData.phoneNumber}
                                onValueChange={(val) => handleChange("phoneNumber", val)}
                                
                            />
                            {error && <p className="text-red-500 text-sm">{error}</p>}
                        </ModalBody>
                        <ModalFooter>
                            <Button color="danger" variant="flat" onPress={onClose}>
                                Close
                            </Button>
                            <Button color="primary" isLoading={isSubmitting} onPress={() => handleCreate(onClose)}>
                                Create
                            </Button>
                        </ModalFooter>
                    </>
                )}
            </ModalContent>
        </Modal>
    );
}