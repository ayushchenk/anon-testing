import { Stack } from "@mui/material";
import { NewQuestion } from "../../../Model/CreateTest/NewQuestion";
import { DisplayQuestion } from "./DisplayQuestion";

interface DisplayQuestionsProps {
    questions: NewQuestion[];
}

export function DisplayQuestions(props: DisplayQuestionsProps) {
    var displays = props.questions.map((question, index) => {
        return (
            <div key={index}>
                <DisplayQuestion question={question} index={index} />
                <hr className="separator" />
            </div>
        );
    });

    return (
        <Stack>{displays}</Stack>
    );
}