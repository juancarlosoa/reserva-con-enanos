import React from "react";
import { Navigate, Outlet } from "react-router-dom";

const useAuth = () => {
  const isAuthenticated = localStorage.getItem("isAuthenticated") === "true";
  const user = JSON.parse(localStorage.getItem("user") || "{}");

  return {
    isAuthenticated,
    user,
    isAdmin: user?.role === "admin",
  };
};

export const PrivateRoute: React.FC = () => {
  const { isAuthenticated } = useAuth();

  if (!isAuthenticated) {
    return <Navigate to="/" replace />;
  }

  return <Outlet />;
};

export const AdminRoute: React.FC = () => {
  const { isAuthenticated, isAdmin } = useAuth();

  if (!isAuthenticated) {
    return <Navigate to="/" replace />;
  }

  if (!isAdmin) {
    return <Navigate to="/dashboard" replace />;
  }

  return <Outlet />;
};

export const PublicRoute: React.FC = () => {
  const { isAuthenticated } = useAuth();

  if (isAuthenticated) {
    return <Navigate to="/dashboard" replace />;
  }

  return <Outlet />;
};
