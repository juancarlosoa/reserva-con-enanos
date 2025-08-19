import { httpRequest } from './httpClient'

export const apiAdapter = {
    get: async <T>(path: string, query?: Record<string, any>): Promise<T> => {
        const res = await httpRequest<T>({ path, method: 'GET', query })
        return res.data
    },
    post: async <T, B = unknown>(path: string, body?: B): Promise<T> => {
        const res = await httpRequest<T, B>({ path, method: 'POST', body })
        return res.data
    },
    put: async <T, B = unknown>(path: string, body?: B): Promise<T> => {
        const res = await httpRequest<T, B>({ path, method: 'PUT', body })
        return res.data
    },
    patch: async <T, B = unknown>(path: string, body?: B): Promise<T> => {
        const res = await httpRequest<T, B>({ path, method: 'PATCH', body })
        return res.data
    },
    delete: async <T = unknown>(path: string): Promise<T> => {
        const res = await httpRequest<T>({ path, method: 'DELETE' })
        return res.data
    }
}

export type ApiAdapter = typeof apiAdapter

