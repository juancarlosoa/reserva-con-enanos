import DefaultLayout from "@/layouts/default";
import AuthForm from "@/components/Auth/AuthForm";
import { Link } from "@heroui/link";
import { loginUser } from "@/api/Auth";
import { useState } from "react";
import { LoginRequestDTO } from "@/dtos/Auth/LoginRequestDTO";
import { useAuth } from "@/contexts/AuthContext";

export default function LoginPage() {
    const [loginResponse, setLoginResponse] = useState("");
    const { login } = useAuth();

    const handleLogin = async (data: Record<string, FormDataEntryValue>) => {
        try {
            const dto: LoginRequestDTO = {
                email: data.email as string,
                password: data.password as string,
            };
            const result = await loginUser(dto);
            setLoginResponse(result);
            login(result.token as string, result)
        } catch (err) {
            setLoginResponse(String(err));
        }
    };

    return (
        <DefaultLayout>
            <section className="flex flex-col items-center justify-center gap-4 py-8 md:py-10">
                <AuthForm mode="login" onSubmit={handleLogin} />
                <p className="mt-4 text-sm text-gray-600">
                    Don't have an account? <Link href="/register" className="text-blue-600">Register</Link>
                </p>
                <p>{JSON.stringify(loginResponse)}</p>
            </section>
        </DefaultLayout>
    );
}