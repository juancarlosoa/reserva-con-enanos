import DefaultLayout from "@/layouts/default";
import AuthForm from "@/components/Auth/AuthForm";
import { Link } from "@heroui/link";
import { loginUser } from "@/api/Auth";
import { useState } from "react";

export default function LoginPage() {
    const [loginResponse, setLoginResponse] = useState("");
    const handleLogin = async (data: Record<string, FormDataEntryValue>) => {
        try {
            const result = await loginUser(data.email as string, data.password as string);
            setLoginResponse(result);
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