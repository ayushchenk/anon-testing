import * as Yup from "yup";
import { NewQuestion, QuestionType } from "../../../Model/CreateTest/NewQuestion";

const validationSchema = Yup.object<Record<keyof NewQuestion, Yup.AnySchema>>({
    content: Yup
        .string()
        .required("Question content is required"),

    questionType: Yup
        .number()
        .required("Type is requried"),

    answers: Yup
        .array()
        .required()
        .test(
            "all-answers-required",
            "Answers are required",
            answers => {
                return answers?.filter(a => !a.content).length === 0;
            }
        )
        .test(
            "answers-for-type",
            "Question should have at least one correct answer",
            function (answers) {
                const correctCount = answers?.filter(a => a.isCorrect && a.content).length;                
                switch (this.parent.questionType) {
                    case QuestionType.String:
                    case QuestionType.Single:
                        return correctCount === 1;
                    case QuestionType.Multiple:
                        return correctCount ? correctCount > 0 : false;
                    default: return false;
                }
            }
        )

});

export default validationSchema;