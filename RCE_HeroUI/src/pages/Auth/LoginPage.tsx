import DefaultLayout from "@/layouts/default";
import AuthForm from "@/components/Auth/AuthForm";
import { Link } from "@heroui/link";
import { AuthService } from "@/services/Auth/AuthService";

export default function LoginPage() {
 
    const handleLogin = async () => {
            try {
              await AuthService.initiateLogin();
            } catch (error) {
              console.error('Login failed:', error);
            }
    };

    return (
        <DefaultLayout>
            <section className="flex flex-col items-center justify-center gap-4 py-8 md:py-10">
                <AuthForm mode="login" onSubmit={handleLogin} />
                <p className="mt-4 text-sm text-gray-600">
                    Don't have an account? <Link href="/register" className="text-blue-600">Register</Link>
                </p>
            </section>
        </DefaultLayout>
    );
}