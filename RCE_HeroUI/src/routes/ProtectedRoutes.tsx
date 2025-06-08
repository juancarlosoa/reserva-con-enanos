import { useAuth } from "@/contexts/AuthContext";
import { Outlet, Navigate } from "react-router-dom";

export const PrivateRoute = () => {
    const { user, loading } = useAuth();

    if (loading) return null;

    return user ? <Outlet /> : <Navigate to="/login" replace />;
};

export const PublicRoute = () => {
    const { user, loading } = useAuth();

    if (loading) return null;

    return !user ? <Outlet /> : <Navigate to="/dashboard" replace />;
};

export const AdminRoute = () => {
    const { user, loading } = useAuth();

    if (loading) return null;

    if (!user) return <Navigate to="/login" replace />;
    if (user?.role !== "admin") return <Navigate to="/dashboard" replace />;
    return <Outlet />;
};