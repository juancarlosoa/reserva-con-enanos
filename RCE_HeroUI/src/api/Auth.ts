// src/api/auth.ts
export async function loginUser(email: string, password: string) {
    const response = await fetch("http://localhost:5106/auth/api/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password }),
    });

    if (!response.ok) {
        throw new Error("Login failed");
    }

    return response.json(); // el token o info que devuelva tu API
}