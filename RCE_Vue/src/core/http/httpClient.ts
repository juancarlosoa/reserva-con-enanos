export type HttpMethod = 'GET' | 'POST' | 'PUT' | 'PATCH' | 'DELETE'

export interface HttpRequestOptions<TBody = unknown> {
    path: string
    method?: HttpMethod
    query?: Record<string, string | number | boolean | undefined | null>
    body?: TBody
    headers?: Record<string, string>
}

export interface HttpResponse<TData> {
    status: number
    ok: boolean
    data: TData
}

function buildQueryString(params?: HttpRequestOptions['query']): string {
    if (!params) return ''
    const usp = new URLSearchParams()
    Object.entries(params).forEach(([key, value]) => {
        if (value === undefined || value === null) return
        usp.append(key, String(value))
    })
    const qs = usp.toString()
    return qs ? `?${qs}` : ''
}

export async function httpRequest<TData = unknown, TBody = unknown>(options: HttpRequestOptions<TBody>): Promise<HttpResponse<TData>> {
    const { path, method = 'GET', query, body, headers } = options

    const response = await fetch(`${path}${buildQueryString(query)}`, {
        method,
        headers: {
            'Content-Type': 'application/json',
            ...(headers || {})
        },
        body: body !== undefined && method !== 'GET' ? JSON.stringify(body) : undefined,
        credentials: 'include'
    })

    let data: any = null
    const contentType = response.headers.get('content-type') || ''
    if (contentType.includes('application/json')) {
        data = await response.json()
    } else {
        data = (await response.text()) as any
    }

    if (!response.ok) {
        const error = new Error(typeof data === 'string' ? data : (data?.message || 'HTTP error')) as any
        error.status = response.status
        error.data = data
        throw error
    }

    return { status: response.status, ok: response.ok, data }
}

