import React from "react";
import { RegisterForm } from "../Auth/Register/RegisterForm";

export class App extends React.Component {
    public render(): React.ReactNode {
        return (
            <RegisterForm onRegister={() => this.registerHandler()} />
        );
    }

    private registerHandler(): void {
        alert("click");
    }
}
