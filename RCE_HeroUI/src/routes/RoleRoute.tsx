import { Navigate } from "react-router-dom";
import { useAuth } from "@/contexts/AuthContext";
import { PrivateRoute } from "./PrivateRoute";

interface Props {
    children: React.ReactNode;
    allowedRoles?: string[];
}

export default function RoleRoute({ children, allowedRoles }: Props) {
    const { user } = useAuth();

    return (
        <PrivateRoute>
            {allowedRoles && user && !allowedRoles.includes(user.role) ? (
                <Navigate to="/dashboard" />
            ) : (
                children
            )}
        </PrivateRoute>
    )
}