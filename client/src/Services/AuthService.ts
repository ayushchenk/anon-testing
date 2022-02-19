import { AuthBase } from "../Model/AuthBase";
import { Response } from "../Model/Response";
import { Token } from "../Model/Token";

export class AuthService {
    private readonly ApiUrl = "https://localhost:7063/api/user";
    private readonly StorageKey = "token";

    public register(model: AuthBase): Promise<Response<Token | undefined>> {
        return this.call(model, "register");
    }

    public login(model: AuthBase): Promise<Response<Token | undefined>> {
        return this.call(model, "login");
    }

    public logout(): void {
        localStorage.clear();
    }

    public getToken(): Token | undefined {
        const tokenJson = localStorage.getItem(this.StorageKey);

        if (tokenJson === null) {
            return undefined;
        }

        return JSON.parse(tokenJson) as Token;
    }

    public isAuthenticated(): boolean {
        const token = this.getToken();

        return token !== undefined && token.expiresOn > new Date();
    }

    private async call(model: AuthBase, path: string): Promise<Response<Token | undefined>> {
        try {

            const response = await fetch(`${this.ApiUrl}/${path}`, {
                headers: {
                    ["Content-Type"]: "application/json"
                },
                method: "POST",
                body: JSON.stringify(model)
            });

            const responseBody = await response.json();

            if (response.ok) {
                console.log(new Date(responseBody.expiresOn));
                console.log(responseBody);

                const token = new Token(responseBody.value, responseBody.userId, new Date(responseBody.expiresOn));
                this.saveToken(token);
                return Response.ok(token);
            }

            return Response.fail(responseBody.error, responseBody.errors);
        }
        catch (error) {
            console.log(error);
            return Response.fail("Unsuccessful request");
        }
    }

    private saveToken(token: Token): void {
        localStorage.setItem(this.StorageKey, JSON.stringify(token));
    }
}