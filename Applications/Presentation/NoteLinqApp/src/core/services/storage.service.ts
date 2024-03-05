import type { AccessTokenDto } from "../models/SecurityDtos";


const accountKey: string = "current-user"
const tokenKey: string = "access-token"

class StorageService {

    SetCurrentAccount(account: AccessTokenDto): void {
        this.set(accountKey, account);
    }

    GetCurrentAccount(): AccessTokenDto {
        return this.get(accountKey) as AccessTokenDto;
    }

    SetAccessToken(token: string): void {
        this.set(tokenKey, token);
    }

    GetAccessToken(): string {
        return this.get(tokenKey) as string;
    }

    IsAuthorize(): boolean {
        return this.get(tokenKey) != null;
    }

    Flush() {
        localStorage.removeItem(accountKey)
        localStorage.removeItem(tokenKey)
    }

    private set(key: string, data: any): void {
        try {
            localStorage.setItem(key, JSON.stringify(data));
        } catch (e) {
            console.error('Error saving to localStorage', e);
        }
    }

    private get(key: string): any | null {
        try {
            let value = localStorage.getItem(key);
            if (value == null) {
                return null
            }
            return JSON.parse(value);
        } catch (e) {
            console.error('Error getting data from localStorage', e);
            return null;
        }
    }

}
const _service = new StorageService();
export default _service

