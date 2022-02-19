import { NewAnswer } from "./NewAnswer";

export interface NewQuestion {
    questionType : QuestionType,
    content: string;
    answers: NewAnswer[];
}

export enum QuestionType {
    Single = 0,
    Multiple = 1,
    String = 2
}