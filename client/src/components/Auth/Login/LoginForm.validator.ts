import * as Yup from "yup";

const validator = Yup.object().shape({
    email: Yup.string()
        .nullable(true)
        .email("Should be valid email")
        .required("Email is required")
        .typeError("Email is required"),

    password: Yup.string()
        .nullable(true)
        .required("Password is required")
        .typeError("Password is required")
});

export default validator;