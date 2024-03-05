export interface LoginDto {
    userName: string,
    password: string,
    rememberMe: boolean,
}

export interface AccessTokenDto {
    name: string,
    userName: string,
    email: string,
    accessToken: string | null
}