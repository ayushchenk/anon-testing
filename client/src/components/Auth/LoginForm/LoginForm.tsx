import { Alert, Button, Collapse, Container, FormLabel, Stack, TextField } from "@mui/material";
import { useFormik } from "formik";
import { useState } from "react";
import { AuthBase } from "../../../Model/AuthBase";
import { AuthService } from "../../../Services/AuthService";
import "../RegisterForm/RegisterForm.css";
import styleProps from "./LoginForm.styles";
import initValues, { AuthFormProps } from "./LoginForm.types";
import valdiationSchema from "./LoginForm.validator";

export function LoginForm(props: AuthFormProps) {
    const [showError, setShowError] = useState(false);
    const [error, setError] = useState("");
    const authService = new AuthService();

    const submitHandler = (values: AuthBase) => {
        authService.login(values).then(response => {
            formik.setSubmitting(false);

            if (response.ok) {
                props.onSubmit(response.value!);
                return;
            }

            setError(response.error!);
            setShowError(true);
            setTimeout(() => setShowError(false), 3500);
        });
    };

    const formik = useFormik({
        initialValues: initValues,
        validationSchema: valdiationSchema,
        validateOnBlur: false,
        onSubmit: submitHandler
    });

    return (
        <Container maxWidth="xs" className="register-form">
            <form onSubmit={formik.handleSubmit} noValidate>
                <Stack>
                    <FormLabel>Login</FormLabel>
                    <TextField
                        {...formik.getFieldProps("email")}
                        {...styleProps}
                        label="Email"
                        error={formik.touched.email && Boolean(formik.errors.email)}
                        helperText={formik.touched.email && formik.errors.email}
                    />
                    <TextField
                        {...formik.getFieldProps("password")}
                        {...styleProps}
                        label="Password"
                        type="password"
                        error={formik.touched.password && Boolean(formik.errors.password)}
                        helperText={formik.touched.password && formik.errors.password}
                    />
                    <Collapse in={showError} className="register-form__error-field">
                        <Alert severity="error"> {error} </Alert>
                    </Collapse>
                    <Button
                        type="submit"
                        disabled={formik.isSubmitting}
                        variant="outlined">
                        Submit
                    </Button>
                </Stack>
            </form>
        </Container>
    );
}