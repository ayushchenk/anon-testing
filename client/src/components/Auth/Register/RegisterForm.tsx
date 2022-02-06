import { Button, Container, FormLabel, Stack, TextField } from "@mui/material";
import React from "react";
import "./RegisterForm.css";

interface RegisterFormProps {
    onRegister: () => void;
}

interface RegisterFormState {
    email: string | undefined;
    password: string | undefined;
    confirmPassword: string | undefined;
}

export class RegisterForm extends React.Component<RegisterFormProps, RegisterFormState>{
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
                        onChange={(event) => this.handleEmailChange(event)}
                    />
                    <TextField
                        variant="outlined"
                        label="Password"
                        type="password"
                        margin="dense"
                        size="small"
                        required
                        onChange={(event) => this.handlePasswordChange(event)}
                    />
                    <TextField
                        variant="outlined"
                        label="Confirm password"
                        type="password"
                        margin="dense"
                        size="small"
                        required
                        onChange={(event) => this.handleConfirmPasswordChange(event)}
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
        if (this.state.email && this.state.password && this.state.confirmPassword) {
            console.log(this.state);
            this.props.onRegister();
        }
    }

    private handleEmailChange(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>): void {
        this.setState({
            email: event.target.value
        });
    }

    private handlePasswordChange(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>): void {
        this.setState({
            password: event.target.value
        });
    }

    private handleConfirmPasswordChange(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>): void {
        this.setState({
            confirmPassword: event.target.value
        });
    }
}