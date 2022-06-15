import { NewTest } from "../Model/CreateTest/NewTest";

export class TestService {
    private readonly ApiUrl = "https://localhost:7063/api/tests";

    public async create(test: NewTest) {
        try {
            const response = await fetch(`${this.ApiUrl}`, {
                body: JSON.stringify(test),
                headers: { "Content-Type": "application/json" },
                method: "POST"
            });

            return response;
        }
        catch (error) {
            console.error(error);
        }
    }
}