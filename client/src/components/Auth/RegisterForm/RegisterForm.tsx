import { Alert, Button, Collapse, Container, FormLabel, Stack, TextField } from "@mui/material";
import { useState } from "react";
import "./RegisterForm.css";
import validationSchema from "./RegisterForm.validator";
import { AuthService } from "../../../Services/AuthService";
import { AuthFormProps } from "../LoginForm/LoginForm.types";
import initValues, { RegisterFormModel } from "./RegisterForm.types";
import { useFormik } from "formik";
import styleProps from "../LoginForm/LoginForm.styles";

export function RegisterForm(props: AuthFormProps) {
    const [showError, setShowError] = useState(false);
    const [error, setError] = useState("");
    const authService = new AuthService();

    const submitHandler = (values: RegisterFormModel) => {
        console.log(values);

        authService.register(values).then(response => {
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
        validationSchema: validationSchema,
        validateOnBlur: false,
        onSubmit: submitHandler
    });

    return (
        <Container maxWidth="xs" className="register-form">
            <form onSubmit={formik.handleSubmit} noValidate>
                <Stack>
                    <FormLabel>Register</FormLabel>
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
                    <TextField
                        {...formik.getFieldProps("confirmPassword")}
                        {...styleProps}
                        label="Confirm password"
                        type="password"
                        value={formik.values.confirmPassword}
                        error={formik.touched.confirmPassword && Boolean(formik.errors.confirmPassword)}
                        helperText={formik.touched.confirmPassword && formik.errors.confirmPassword}
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