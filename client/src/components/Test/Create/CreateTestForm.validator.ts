import * as Yup from "yup";
import { NewTest } from "../../../Model/CreateTest/NewTest";

const validationSchema = Yup.object<Record<keyof NewTest, Yup.AnySchema>>({
    title: Yup
        .string()
        .required("Title is required"),

    userId: Yup
        .string()
        .optional(),

    questions: Yup
        .array()
        .min(1, "Test should have at least one question")
});

export default validationSchema;