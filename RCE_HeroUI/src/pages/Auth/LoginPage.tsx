import DefaultLayout from "@/layouts/default";
import { Link } from "@heroui/link";
import { AuthService } from "@/services/Auth/AuthService";
import { Button } from "@heroui/button";
import { useAuth } from "@/auth/AuthProvider";

export default function LoginPage() {
    const { user, login, logout } = useAuth();

    return (
        <DefaultLayout>
            <section className="flex flex-col items-center justify-center gap-4 py-8 md:py-10">
                <div className="flex gap-2">
                    <button onClick={login}>Iniciar sesión</button>
                </div>

                <p className="mt-4 text-sm text-gray-600">
                    Don't have an account? <Link href="/register" className="text-blue-600">Register</Link>
                </p>
            </section>
        </DefaultLayout>
    );
}