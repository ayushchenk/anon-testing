import React from "react";
import { Route, Routes } from "react-router-dom";
import AppContext, { ApplicationContext } from "../../Contexts/AppContext";
import { Token } from "../../Model/Token";
import { AuthService } from "../../Services/AuthService";
import { LoginForm } from "../Auth/Login/LoginForm";
import { RegisterForm } from "../Auth/Register/RegisterForm";
import { Header } from "../Header/Header";
import { CreateTestForm } from "../Test/Create/CreateTestForm";

interface AppState extends ApplicationContext {
}

export class App extends React.Component<{}, AppState> {
    private readonly authService = new AuthService();

    public constructor(props: {}) {
        super(props);
        this.state = {
            isAuthed: this.authService.isAuthenticated()
        }
    }

    public render(): React.ReactNode {
        const loginForm = <LoginForm onLogin={(token) => this.loginHandler(token)} />;
        const registerForm = <RegisterForm onRegister={(token) => this.registerHandler(token)} />;
        const createTestForm = <CreateTestForm />;

        return (
            <AppContext.Provider value={this.state}>
                <Header />
                <Routes>
                    <Route path="/login" element={loginForm} />
                    <Route path="/register" element={registerForm} />
                    <Route path="/create" element={createTestForm} />
                </Routes>
            </AppContext.Provider>
        );
    }

    private registerHandler(token: Token): void {
        console.log("register");
        console.log(token);
    }

    private loginHandler(token: Token): void {
        console.log("login");
        console.log(token);
    }
}
