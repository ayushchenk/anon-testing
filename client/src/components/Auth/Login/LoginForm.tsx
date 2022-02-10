import { Alert, Button, Collapse, Container, FormLabel, Stack, TextField } from "@mui/material";
import React from "react";
import { ValidationError } from "yup";
import { ErrorResponse } from "../../../Model/ErrorResponse";
import { Token } from "../../../Model/Token";
import { AuthService } from "../../../Services/AuthService";
import "../Register/RegisterForm.css";
import validator from "./LoginForm.validator";

export interface LoginFormProps {
    onLogin: (token: Token) => void;
}

export interface LoginFormState {
    email: string;
    password: string;
    valdiationEnabled: boolean;
    error: string;
    showError: boolean;
    emailValidation?: string;
    passwordValidation?: string;
}

export class LoginForm extends React.Component<LoginFormProps, LoginFormState> {
    private readonly authService = new AuthService();

    public constructor(props: LoginFormProps) {
        super(props);

        this.state = {
            email: "",
            password: "",
            error: "",
            showError: false,
            valdiationEnabled: false
        };
    }

    public render(): React.ReactNode {
        return (
            <Container maxWidth="xs" className="register-form">
                <Stack>
                    <FormLabel>Login</FormLabel>
                    <TextField
                        variant="outlined"
                        label="Email"
                        size="small"
                        margin="dense"
                        required
                        error={this.state.emailValidation !== undefined && this.state.valdiationEnabled}
                        helperText={this.state.valdiationEnabled ? this.state.emailValidation : ""}
                        onChange={(event) => this.handleFieldChange(event, "email")}
                    />
                    <TextField
                        variant="outlined"
                        label="Password"
                        type="password"
                        margin="dense"
                        size="small"
                        required
                        error={this.state.passwordValidation !== undefined && this.state.valdiationEnabled}
                        helperText={this.state.valdiationEnabled ? this.state.passwordValidation : ""}
                        onChange={(event) => this.handleFieldChange(event, "password")}
                    />
                    <Collapse in={this.state.showError} className="register-form__error-field">
                        <Alert severity="error"> {this.state.error} </Alert>
                    </Collapse>
                    <Button
                        onClick={() => this.handleSubmit()}
                        variant="outlined">
                        Submit
                    </Button>
                </Stack>
            </Container>
        );
    }

    private handleSubmit(): void {
        this.setState({
            valdiationEnabled: true
        }, this.validateEndSend);
    }

    private validateEndSend(): void {
        if (this.validateInput()) {
            this.authService.login({
                email: this.state.email,
                password: this.state.password
            }).then(response => {
                console.log(response);

                if (response instanceof Token) {
                    this.props.onLogin(response);
                    return;
                }

                if (response instanceof ErrorResponse) {
                    this.setState({
                        error: response.error!,
                        showError: true
                    }, () => setTimeout(
                        () => this.setState({ showError: false }),
                        5000)
                    );
                }
            });
        }
    }

    private validateInput(): boolean {
        if (this.state.valdiationEnabled) {
            try {
                validator.validateSync(this.state, { abortEarly: false });

                this.setState({
                    emailValidation: undefined,
                    passwordValidation: undefined
                });

                return true;
            }
            catch (error) {
                if (error instanceof ValidationError && error.inner) {
                    this.setState({
                        emailValidation: error.inner.find(e => e.path === "email")?.message,
                        passwordValidation: error.inner.find(e => e.path === "password")?.message,
                    });
                }

                return false;
            }
        }

        return false;
    }

    private handleFieldChange(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>, key: keyof LoginFormState): void {
        this.setState((state) => {
            return {
                ...state,
                [key]: event.target.value
            };
        }, this.validateInput);
    }
}