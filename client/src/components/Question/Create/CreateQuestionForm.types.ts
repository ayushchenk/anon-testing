import { NewQuestion, QuestionType } from "../../../Model/CreateTest/NewQuestion";

export interface CreateQuestionFormProps {
    onSave: (question: NewQuestion) => void;
    onCancel: () => void;
}

const initValues: NewQuestion = {
    content: "",
    questionType: QuestionType.Single,
    answers: []
}

export default initValues;