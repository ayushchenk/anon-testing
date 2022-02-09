import { Button, Container, FormLabel, Stack, TextField } from "@mui/material";
import React from "react";
import validator from "./RegisterForm.validator";
import "./RegisterForm.css";
import { ValidationError } from "yup";

interface RegisterFormProps {
    onRegister: () => void;
}

interface RegisterFormState {
    email?: string;
    password?: string;
    confirmPassword?: string;
    valdiationEnabled: boolean;
    emailValidation?: string;
    passwordValidation?: string;
    confirmPasswordValidation?: string;
}

export class RegisterForm extends React.Component<RegisterFormProps, RegisterFormState>{
    public constructor(props: RegisterFormProps) {
        super(props);

        this.state = {
            valdiationEnabled: false
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
        if (!this.state.valdiationEnabled) {
            this.setState({
                valdiationEnabled: true
            }, this.validateInput);
        }

        if(this.validateInput()){
            alert("valid!");
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