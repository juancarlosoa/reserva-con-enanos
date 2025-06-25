import React, { createContext, useEffect, useState, useContext } from 'react';
import { userManager } from './userManager';
import type { User } from 'oidc-client-ts';

const AuthContext = createContext<{
    user: User | null;
    login: () => void;
    logout: () => void;
}>({ user: null, login: () => { }, logout: () => { } });

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
    const [user, setUser] = useState<User | null>(null);

    useEffect(() => {
        userManager.getUser().then(setUser);
    }, []);

    const login = () => {
        console.log("Iniciando redirección OIDC...");
        userManager.signinRedirect();
    }
    const logout = () => userManager.signoutRedirect();

    return (
        <AuthContext.Provider value={{ user, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);