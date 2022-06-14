import * as Yup from "yup";

const validationSchema = Yup.object().shape({
    email: Yup.string()
        .nullable(true)
        .email("Should be valid email")
        .required("Email is required")
        .typeError("Email is required"),

    password: Yup.string()
        .nullable(true)
        .matches(/^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$/, "Password must have at leat one upper-case letter, one digit and only consist of letters and digits")
        .required("Password is required")
        .typeError("Password is required"),

    confirmPassword: Yup.string()
        .nullable(true)
        .required("Password is required")
        .typeError("Password is required")
        .oneOf([Yup.ref("password")], "Passwords must match")
});

export default validationSchema;