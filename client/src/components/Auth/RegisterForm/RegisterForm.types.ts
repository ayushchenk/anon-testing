import { AuthBase } from "../../../Model/AuthBase";

export interface RegisterFormModel extends AuthBase {
    confirmPassword: string;
}

const initValues: RegisterFormModel = {
    email: "",
    password: "",
    confirmPassword: ""
};

export default initValues;