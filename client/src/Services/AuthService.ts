import { AuthBase } from "../Model/AuthBase";
import { ErrorResponse } from "../Model/ErrorResponse";
import { Token } from "../Model/Token";

export class AuthService {
    private readonly ApiUrl = "https://localhost:7063/api/user";

    public register(model: AuthBase): Promise<Token | ErrorResponse> {
        return this.call(model, "register");
    }

    public login(model: AuthBase): Promise<Token | ErrorResponse> {
        return this.call(model, "login");
    }

    private async call(model: AuthBase, path: string): Promise<Token | ErrorResponse> {
        try {

            const response = await fetch(`${this.ApiUrl}/${path}`, {
                headers: {
                    ["Content-Type"]: "application/json"
                },
                method: "POST",
                body: JSON.stringify(model)
            });

            const responseBody = await response.json();

            return response.ok
                ? new Token(responseBody.value, responseBody.userId, responseBody.expiresOn)
                : new ErrorResponse(responseBody.error, responseBody.errors);
        }
        catch (error) {
            console.log(error);
            return new ErrorResponse("Unsuccessful request"); 
        }
    }
}