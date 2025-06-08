import { useAuth } from "@/contexts/AuthContext";
import DefaultLayout from "@/layouts/default";

export default function DashboardPage() {
    const { user } = useAuth();
    const { logout } = useAuth();

    const handleLogout = async () => {
        try {

            logout();
        } catch (err) {

        }
    };

    return (
        <DefaultLayout>
            <div>
                <h1>Bienvenido </h1>
                <p>{JSON.stringify(user)}</p>
                <p>Tu email: {user?.email}</p>
                <p>Tu rol: {user?.role}</p>
                <button onClick={handleLogout} > Cerrar Sesion </button>
            </div>
        </DefaultLayout>
    );
}