import { Configuration } from '@/services/api';

export class OpenApiConfig {
    static getConfig(accessToken = '') {
        const basePath = process.env.NEXT_PUBLIC_API_BASE_URL || '';
        return new Configuration({
            basePath,
            accessToken: () => `Bearer ${accessToken}`,
        });
    }
}
