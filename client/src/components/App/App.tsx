import React from "react";
import { BrowserRouter, Link, Route, Routes } from "react-router-dom";
import { Token } from "../../Model/Token";
import { LoginForm } from "../Auth/Login/LoginForm";
import { RegisterForm } from "../Auth/Register/RegisterForm";

export class App extends React.Component {
    public render(): React.ReactNode {
        const loginForm = <LoginForm onLogin={(token) => this.loginHandler(token)} />;
        const registerForm = <RegisterForm onRegister={(token) => this.registerHandler(token)} />;

        return (
            <BrowserRouter>
                <Link to="/login" >Login</Link>
                <Link to="/register" >Register</Link>
                <Routes>
                    <Route path="/login" element={loginForm} />
                    <Route path="/register" element={registerForm} />
                    <Route path="*" element={null} />
                </Routes>
            </BrowserRouter>
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
