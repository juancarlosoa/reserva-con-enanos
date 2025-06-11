import DefaultLayout from "@/layouts/default";
import AuthForm from "@/components/Auth/AuthForm";
import { Link } from "@heroui/link";
import { useState } from "react";
import { registerUser } from "@/api/Auth";
import { RegisterRequestDTO } from "@/dtos/Auth/RegisterRequestDTO";

export default function RegisterPage() {
    const [registerResponse, setRegisterResponse] = useState("");
    const handleRegister = async (data: Record<string, FormDataEntryValue>) => {
        try {
            const dto: RegisterRequestDTO = {
                email: data.email as string,
                password: data.password as string,
                confirmPassword: data.confirmPassword as string,
                role: data.role as string
            }
            const result = await registerUser(dto);
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
                <p>{JSON.stringify(registerResponse)}</p>
            </section>
        </DefaultLayout>
    );
}