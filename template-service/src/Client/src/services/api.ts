interface RequestOptions extends RequestInit {
    body?: any;
}
class ApiService {
    private baseUrl: string;

    constructor() {
        // TODO: move to .env
        this.baseUrl =
            process.env.NODE_ENV === 'production'
                ? 'https://code-dealers-api-b9cyakamagapb0h2.westeurope-01.azurewebsites.net'
                : 'http://localhost:7543';
    }
    private async request(endpoint: string, options: RequestOptions = {}): Promise<any> {
        try {
            const response = await fetch(`${this.baseUrl}${endpoint}`, {
                ...options,
                headers: {
                    'Content-Type': 'application/json',
                    ...options.headers,
                },
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(
                    `HTTP error! Status: ${response.status}, Message: ${
                        errorData.message || 'Unknown error'
                    }`
                );
            }

            return response.json();
        } catch (error) {
            console.error('Request error:', error);
            throw error;
        }
    }

    public async get(endpoint: string, params?: Record<string, string>): Promise<any> {
        const queryString = params ? '?' + new URLSearchParams(params).toString() : '';
        return this.request(`${endpoint}${queryString}`, { method: 'GET' });
    }

    public async post(endpoint: string, body: any): Promise<any> {
        return this.request(endpoint, {
            method: 'POST',
            body: JSON.stringify(body),
        });
    }

    public async fetch(
        endpoint: string,
        method: 'GET' | 'POST',
        body?: any,
        headers: Record<string, string> = {}
    ): Promise<any> {
        return this.request(endpoint, {
            method,
            body: body ? JSON.stringify(body) : undefined,
            headers,
        });
    }
}

export class ApiServiceWithState {
    private apiService: ApiService;
    public isLoading: boolean = false;
    public error: Error | null = null;

    constructor() {
        this.apiService = new ApiService();
    }

    private setLoading(loading: boolean) {
        this.isLoading = loading;
    }

    private setError(error: Error | null) {
        this.error = error;
    }

    public async get(endpoint: string, params?: Record<string, string>): Promise<any> {
        this.setLoading(true);
        try {
            const data = await this.apiService.get(endpoint, params);
            this.setError(null);
            return data;
        } catch (error) {
            this.setError(error as Error);
            throw error;
        } finally {
            this.setLoading(false);
        }
    }

    public async post(endpoint: string, body: any): Promise<any> {
        this.setLoading(true);
        try {
            const data = await this.apiService.post(endpoint, body);
            this.setError(null);
            return data;
        } catch (error) {
            this.setError(error as Error);
            throw error;
        } finally {
            this.setLoading(false);
        }
    }

    public async fetch(
        endpoint: string,
        method: 'GET' | 'POST',
        body?: any,
        headers: Record<string, string> = {}
    ): Promise<any> {
        this.setLoading(true);
        try {
            const data = await this.apiService.fetch(endpoint, method, body, headers);
            this.setError(null);
            return data;
        } catch (error) {
            this.setError(error as Error);
            throw error;
        } finally {
            this.setLoading(false);
        }
    }
}

const apiServiceWithState = new ApiServiceWithState();
export default apiServiceWithState;
