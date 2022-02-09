import { Alert, Button, Collapse, Container, FormLabel, Stack, TextField } from "@mui/material";
import React from "react";
import "./RegisterForm.css";
import { ValidationError } from "yup";
import validator from "./RegisterForm.validator";
import { AuthService, IAuthService } from "../../../Services/AuthService";
import { Token } from "../../../Model/Token";

interface RegisterFormProps {
    onRegister: (token: Token) => void;
}

interface RegisterFormState {
    email: string;
    password: string;
    confirmPassword: string;
    valdiationEnabled: boolean;
    showError: boolean;
    error: string;
    emailValidation?: string;
    passwordValidation?: string;
    confirmPasswordValidation?: string;
}

export class RegisterForm extends React.Component<RegisterFormProps, RegisterFormState>{
    private readonly authService: IAuthService = new AuthService();

    public constructor(props: RegisterFormProps) {
        super(props);

        this.state = {
            email: "",
            password: "",
            confirmPassword: "",
            error: "",
            valdiationEnabled: false,
            showError: false
        };
    }

    public render(): React.ReactNode {
        return (
            <Container maxWidth="xs" className="register-form">
                <Stack>
                    <FormLabel>Register</FormLabel>
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
                    <TextField
                        variant="outlined"
                        label="Confirm password"
                        type="password"
                        margin="dense"
                        size="small"
                        required
                        error={this.state.confirmPasswordValidation !== undefined && this.state.valdiationEnabled}
                        helperText={this.state.valdiationEnabled ? this.state.confirmPasswordValidation : ""}
                        onChange={(event) => this.handleFieldChange(event, "confirmPassword")}
                    />
                    <Collapse in={this.state.showError} className="registe-form__error-field">
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
            this.authService.register({
                email: this.state.email,
                password: this.state.password
            }).then(response => {
                if (response instanceof Token) {
                    this.props.onRegister(response);
                    return;
                }

                this.setState({
                    error: response.error!,
                    showError: true
                }, () => setTimeout(
                    () => this.setState({ showError: false }),
                    5000)
                );

                console.log(response);
            });
        }
    }

    private validateInput(): boolean {
        if (this.state.valdiationEnabled) {
            try {
                validator.validateSync(this.state, { abortEarly: false });

                this.setState({
                    emailValidation: undefined,
                    passwordValidation: undefined,
                    confirmPasswordValidation: undefined
                });

                return true;
            }
            catch (error) {
                if (error instanceof ValidationError && error.inner) {
                    this.setState({
                        emailValidation: error.inner.find(e => e.path === "email")?.message,
                        passwordValidation: error.inner.find(e => e.path === "password")?.message,
                        confirmPasswordValidation: error.inner.find(e => e.path === "confirmPassword")?.message,
                    });
                }

                return false;
            }
        }

        return false;
    }

    private handleFieldChange(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>, key: keyof RegisterFormState): void {
        this.setState((state) => {
            return {
                ...state,
                [key]: event.target.value
            };
        }, this.validateInput);
    }
}