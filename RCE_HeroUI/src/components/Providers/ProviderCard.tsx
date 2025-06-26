import { Card, CardBody } from "@heroui/react";
import { useNavigate } from "react-router-dom";

type Provider = {
    id: string;
    name: string;
    description: string;
    imageUrl: string;
    createdAt: string;
};

export const ProviderCard = ({ provider }: { provider: Provider }) => {
    const navigate = useNavigate();

    return (
        <Card
            className="cursor-pointer hover:shadow-lg transition"
            onClick={() => navigate(`/providers/${provider.id}`)}>
            <img
                src={provider.imageUrl}
                alt={provider.name}
                className="h-48 w-full object-cover rounded-t-2xl"
            />
            <CardBody className="p-4">
                <h3 className="text-xl font-semibold">{provider.name}</h3>
                <p className="text-sm text-gray-500">
                    {new Date(provider.createdAt).toLocaleDateString()}
                </p>
                <p className="text-sm mt-2">{provider.description}</p>
            </CardBody>
        </Card>
    );
};