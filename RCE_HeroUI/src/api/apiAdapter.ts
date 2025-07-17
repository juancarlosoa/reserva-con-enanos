import axios, { AxiosError, AxiosInstance, AxiosRequestConfig, AxiosResponse, InternalAxiosRequestConfig } from "axios";

export class ApiAdapter {
    private client: AxiosInstance;

    constructor(baseURL: string) {
        this.client = axios.create({
            baseURL,
        });
    }

    async get<T = any>(
        path: string,
        config?: AxiosRequestConfig
    ): Promise<T> {
        const response: AxiosResponse<T> = await this.client.get(path, config);
        return response.data;
    }

    async post<T = any>(
        path: string,
        data?: any,
        config?: AxiosRequestConfig
    ): Promise<T> {
        const response: AxiosResponse<T> = await this.client.post(path, data, config);
        return response.data;
    }

    async put<T = any>(
        path: string,
        data?: any,
        config?: AxiosRequestConfig
    ): Promise<T> {
        const response: AxiosResponse<T> = await this.client.put(path, data, config);
        return response.data;
    }

    async delete<T = any>(
        path: string,
        config?: AxiosRequestConfig
    ): Promise<T> {
        const response: AxiosResponse<T> = await this.client.delete(path, config);
        return response.data;
    }

    addRequestInterceptor(
        onFulfilled: (value: InternalAxiosRequestConfig) => InternalAxiosRequestConfig | Promise<InternalAxiosRequestConfig>,
        onRejected?: (error: any) => any
    ) {
        this.client.interceptors.request.use(onFulfilled, onRejected);
    }

    addResponseInterceptor(
        onFulfilled: (value: AxiosResponse) => AxiosResponse | Promise<AxiosResponse>,
        onRejected?: (error: AxiosError) => any
    ) {
        this.client.interceptors.response.use(onFulfilled, onRejected);
    }
}