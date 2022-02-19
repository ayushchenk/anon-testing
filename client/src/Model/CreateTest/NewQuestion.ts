import { NewAnswer } from "./NewAnswer";

export interface NewQuestion {
    questionType: QuestionType,
    content: string;
    answers: NewAnswer[];
}

export enum QuestionType {
    Single = 0,
    Multiple = 1,
    String = 2
}

export function questionTypeDescription(type: QuestionType): string {
    switch (type) {
        case QuestionType.Single: return "Single correct answer";
        case QuestionType.Multiple: return "Multiple correct answers";
        case QuestionType.String: return "Correct answer as string value";
        default: return "";
    }
}