export interface User {
    email: string;
    role: Role
}

export const Roles = {
    Room: "room",
    Provider: "provider"
}

export type Role = typeof Roles[keyof typeof Roles];