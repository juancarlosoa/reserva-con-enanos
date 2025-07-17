import { useAuth } from "@/auth/AuthProvider";
import { Outlet, Navigate } from "react-router-dom";

export const PrivateRoute = () => {
    const { user } = useAuth();

    return user ? <Outlet /> : <Navigate to="/" replace />;
};

export const AdminRoute = () => {
    const { user } = useAuth();

    if (!user) return <Navigate to="/" replace />;
    if (user?.scope !== "admin") return <Navigate to="/dashboard" replace />;
    return <Outlet />;
};