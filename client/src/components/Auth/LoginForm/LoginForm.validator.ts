import * as Yup from "yup";
import { AuthBase } from "../../../Model/AuthBase";

const valdiationSchema = Yup.object<Record<keyof AuthBase, Yup.AnySchema>>({
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

export default valdiationSchema;