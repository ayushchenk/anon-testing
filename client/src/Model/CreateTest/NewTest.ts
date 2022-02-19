import { NewQuestion } from "./NewQuestion";

export interface NewTest {
    userId: string,
    title: string,
    questions: NewQuestion[]
}