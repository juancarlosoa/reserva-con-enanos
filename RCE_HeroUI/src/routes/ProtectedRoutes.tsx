import { useAuth } from "@/auth/AuthProvider";
import { Outlet, Navigate } from "react-router-dom";

export const PrivateRoute = () => {
    const { user } = useAuth();

    return user ? <Outlet /> : <Navigate to="/login" replace />;
};

export const PublicRoute = () => {
    const { user } = useAuth();

    return !user ? <Outlet /> : <Navigate to="/dashboard" replace />;
};

export const AdminRoute = () => {
    const { user } = useAuth();

    if (!user) return <Navigate to="/login" replace />;
    if (user?.scope !== "admin") return <Navigate to="/dashboard" replace />;
    return <Outlet />;
};