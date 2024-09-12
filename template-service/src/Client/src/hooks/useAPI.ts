import { useState, useCallback } from 'react';
import apiServiceWithState from '../services/api';

type UseApiResult<T> = {
    data: T | null;
    isLoading: boolean;
    error: Error | null;
    fetch: (body?: any) => Promise<any>;
};

const useApi = <T>(endpoint: string, method: 'GET' | 'POST' = 'GET'): UseApiResult<T> => {
    const [data, setData] = useState<T | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<Error | null>(null);

    const fetch = useCallback(
        async (body?: any) => {
            setIsLoading(true);
            setError(null);
            try {
                const result =
                    method === 'POST'
                        ? await apiServiceWithState.post(endpoint, body)
                        : await apiServiceWithState.get(endpoint);

                setData(result);
                return result;
            } catch (error) {
                setError(error as Error);
                throw error;
            } finally {
                setIsLoading(false);
            }
        },
        [endpoint, method]
    );

    return { data, isLoading, error, fetch };
}

export default useApi;