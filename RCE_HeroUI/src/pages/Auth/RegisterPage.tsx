import DefaultLayout from "@/layouts/default";
import AuthForm from "@/components/Auth/AuthForm";
import { Link } from "@heroui/link";
import { useState } from "react";
import { loginUser } from "@/api/Auth";

export default function RegisterPage() {
    const [registerResponse, setRegisterResponse] = useState("");
    const handleRegister = async (data: Record<string, FormDataEntryValue>) => {
        try {
            const result = await loginUser(data.user as string, data.email as string);
            setRegisterResponse(result);
        } catch (err) {
            setRegisterResponse(String(err));
        }
    };
    return (
        <DefaultLayout>
            <section className="flex flex-col items-center justify-center gap-4 py-8 md:py-10">
                <AuthForm mode="register" onSubmit={handleRegister} />
                <p className="mt-4 text-sm text-gray-600">
                    Already have an account? <Link href="/login" className="text-blue-600">Login</Link>
                </p>
                <p>{registerResponse}</p>
            </section>
        </DefaultLayout>
    );
}