import { NewQuestion, QuestionType } from "../../../Model/CreateTest/NewQuestion";

export class QuestionValidator {
    public validate(question: NewQuestion): Map<string, string> {
        const result = new Map<string, string>();

        if (!question.content) {
            result.set("content", "Content is required");
        }

        if (!question.answers) {
            result.set("answers", "Answers is required");
        }

        if (question.answers) {
            const emptyCount = question.answers.filter(a => !a.content).length;
            if (emptyCount > 0) {
                result.set("answers", "Answers cannot be empty");
            }

            const correctCount = question.answers.filter(a => a.isCorrect).length;
            switch (question.questionType) {
                case QuestionType.Single:
                    if (correctCount != 1) {
                        result.set("answers", "Question should have one correct answer");
                    }
                    break;
                case QuestionType.Multiple:
                    if (correctCount === 0) {
                        result.set("answers", "Question should have at least one correct answer");
                    }
                    break;
                case QuestionType.String:
                    if (question.answers.length != 1) {
                        result.set("answers", "Question should have one answer");
                    }
                    break;
                default:
                    result.set("answers", "Question Type is required");
            }
        }

        return result;
    }
}