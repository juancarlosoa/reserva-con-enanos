import { useParams } from "react-router-dom";

export const ProviderDetail = () => {
    const { id } = useParams();

    return (
        <div className="p-6">
            <h2 className="text-2xl font-bold">Detalle del proveedor</h2>
            <p className="mt-4">ID: {id}</p>
        </div>
    );
};