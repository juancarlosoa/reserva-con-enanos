import React, {
  createContext,
  useEffect,
  useState,
  useContext,
  useCallback,
} from "react";
import { userManager } from "./userManager";
import type { User } from "oidc-client-ts";

interface AuthContextType {
  user: User | null;
  isLoading: boolean;
  isAuthenticated: boolean;
  login: () => Promise<void>;
  logout: () => Promise<void>;
  error: string | null;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [user, setUser] = useState<User | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const initializeAuth = async () => {
      try {
        setIsLoading(true);
        setError(null);
        const currentUser = await userManager.getUser();
        setUser(currentUser);
      } catch (err) {
        console.error("Error al obtener usuario:", err);
        setError("Error al verificar autenticación");
      } finally {
        setIsLoading(false);
      }
    };

    initializeAuth();
  }, []);

  useEffect(() => {
    const handleUserLoaded = (user: User) => {
      console.log("Usuario cargado:", user.profile);
      setUser(user);
      setError(null);
    };

    const handleUserUnloaded = () => {
      console.log("Usuario desconectado");
      setUser(null);
    };

    const handleAccessTokenExpired = () => {
      console.log("Token expirado");
      setError("Sesión expirada");
    };

    const handleSilentRenewError = (error: Error) => {
      console.error("Error en renovación silenciosa:", error);
      setError("Error al renovar sesión");
    };

    userManager.events.addUserLoaded(handleUserLoaded);
    userManager.events.addUserUnloaded(handleUserUnloaded);
    userManager.events.addAccessTokenExpired(handleAccessTokenExpired);
    userManager.events.addSilentRenewError(handleSilentRenewError);

    return () => {
      userManager.events.removeUserLoaded(handleUserLoaded);
      userManager.events.removeUserUnloaded(handleUserUnloaded);
      userManager.events.removeAccessTokenExpired(handleAccessTokenExpired);
      userManager.events.removeSilentRenewError(handleSilentRenewError);
    };
  }, []);

  const login = useCallback(async () => {
    try {
      setError(null);
      console.log("Iniciando redirección OIDC...");
      await userManager.signinRedirect();
    } catch (err) {
      console.error("Error en login:", err);
      setError("Error al iniciar sesión");
    }
  }, []);

  const logout = useCallback(async () => {
    try {
      setError(null);
      await userManager.signoutRedirect();
    } catch (err) {
      console.error("Error en logout:", err);
      setError("Error al cerrar sesión");
    }
  }, []);

  const value: AuthContextType = {
    user,
    isLoading,
    isAuthenticated: !!user && !user.expired,
    login,
    logout,
    error,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = (): AuthContextType => {
  const context = useContext(AuthContext);

  if (context === undefined) {
    throw new Error("useAuth debe usarse dentro de un AuthProvider");
  }

  return context;
};

export const useRequireAuth = () => {
  const { isAuthenticated, isLoading } = useAuth();

  return {
    isAuthenticated,
    isLoading,
    canAccess: isAuthenticated && !isLoading,
  };
};
