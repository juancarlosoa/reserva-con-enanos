export const REPOSITORY_HOSTS = {
    PROVIDERS: '/api/providers',
    ROOMS: '/api/rooms'
} as const

export type RepositoryHostKey = keyof typeof REPOSITORY_HOSTS

