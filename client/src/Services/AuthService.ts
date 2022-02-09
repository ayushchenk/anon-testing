import { AuthBase } from "../Model/AuthBase";
import { ErrorResponse } from "../Model/ErrorResponse";
import { Token } from "../Model/Token";

export interface IAuthService {
    register(model: AuthBase): Promise<Token | ErrorResponse>;
}

export class AuthService implements IAuthService {
    private readonly ApiUrl = "https://localhost:7063/api/user";

    public async register(model: AuthBase): Promise<Token | ErrorResponse> {
        const response = await fetch(`${this.ApiUrl}/register`, {
            headers: {
                ["Content-Type"]: "application/json"
            },
            method: "POST",
            body: JSON.stringify(model)
        });

        const responseBody = await response.json();

        return response.ok
            ? responseBody as Token
            : responseBody as ErrorResponse;
    }
}