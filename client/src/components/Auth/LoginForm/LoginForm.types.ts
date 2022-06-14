import { AuthBase } from "../../../Model/AuthBase";
import { Token } from "../../../Model/Token";

export interface AuthFormProps {
    onSubmit: (token: Token) => void;
}

const initValues: AuthBase = {
    email: "",
    password: ""
};

export default initValues;