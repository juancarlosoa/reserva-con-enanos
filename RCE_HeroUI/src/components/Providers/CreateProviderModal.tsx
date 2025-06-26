import { createProvider } from "@/services/ProviderService";
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


export const MailIcon = (props: React.SVGProps<SVGSVGElement>) => {
    return (
        <svg
            aria-hidden="true"
            fill="none"
            focusable="false"
            height="1em"
            role="presentation"
            viewBox="0 0 24 24"
            width="1em"
            {...props}
        >
            <path
                d="M17 3.5H7C4 3.5 2 5 2 8.5V15.5C2 19 4 20.5 7 20.5H17C20 20.5 22 19 22 15.5V8.5C22 5 20 3.5 17 3.5ZM17.47 9.59L14.34 12.09C13.68 12.62 12.84 12.88 12 12.88C11.16 12.88 10.31 12.62 9.66 12.09L6.53 9.59C6.21 9.33 6.16 8.85 6.41 8.53C6.67 8.21 7.14 8.15 7.46 8.41L10.59 10.91C11.35 11.52 12.64 11.52 13.4 10.91L16.53 8.41C16.85 8.15 17.33 8.2 17.58 8.53C17.84 8.85 17.79 9.33 17.47 9.59Z"
                fill="currentColor"
            />
        </svg>
    );
};

interface Props {
    isOpen: boolean;
    onOpenChange: (open: boolean) => void;
}

export default function CreateProviderModal({ isOpen, onOpenChange }: Props) {
    const [formData, setFormData] = useState({
        name: "",
        email: "",
        phone: "",
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
            await createProvider(formData);
            onClose(); // Cierra el modal si todo fue bien
            setFormData({ name: "", email: "", phone: "" }); // Limpia el formulario
        } catch (err) {
            console.error(err);
            setError("Hubo un error al crear el proveedor.");
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <Modal isOpen={isOpen} placement="top-center" onOpenChange={onOpenChange} backdrop="blur">
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
                            />
                            <Input
                                label="Email"
                                placeholder="Enter email"
                                type="email"
                                variant="bordered"
                                endContent={
                                    <MailIcon className="text-2xl text-default-400 pointer-events-none flex-shrink-0" />
                                }
                            />
                            <Input
                                label="Phone number"
                                placeholder="Enter phone number"
                                type="tel"
                                variant="bordered"
                                value={formData.phone}
                                onValueChange={(val) => handleChange("phone", val)} />

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