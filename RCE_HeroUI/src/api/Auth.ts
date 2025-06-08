import { LoginRequestDTO } from "@/dtos/Auth/LoginRequestDTO";
import { RegisterRequestDTO } from "@/dtos/Auth/RegisterRequestDTO";

const urlBase = "http://localhost:5101/auth/api/";

export async function loginUser(dto: LoginRequestDTO) {
    const response = await fetch(urlBase + "login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(dto),
    });

    if (!response.ok) {
        throw new Error("Login failed");
    }

    return response.json();
}

export async function registerUser(dto: RegisterRequestDTO) {
    const response = await fetch(urlBase + "register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(dto),
    });

    if (!response.ok) {
        throw new Error("Register failed");
    }

    return response.json();
}