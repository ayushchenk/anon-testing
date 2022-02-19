import { FormControl, TextField } from "@mui/material";
import { NewQuestion, questionTypeDescription } from "../../../Model/CreateTest/NewQuestion";
import { DisplayAnswers } from "../../Answer/Display/DisplayAnswers";
import "./../../Answer/Display/DisplayAnswer.css";

interface DisplayQuestionProps {
    question: NewQuestion;
    index: number;
}

export function DisplayQuestion(props: DisplayQuestionProps) {
    return (
        <FormControl fullWidth={true}>
            {/* <TextField
                variant="standard"
                size="small"
                margin="dense"
                multiline={true}
                value={`${props.index + 1}. ${props.question.content}`}
                maxRows={Infinity}
                inputProps={
                    { readOnly: true }
                } />
            <TextField
                variant="standard"
                size="small"
                margin="dense"
                multiline={true}
                value={questionTypeDescription(props.question.questionType)}
                maxRows={Infinity}
                inputProps={
                    { readOnly: true }
                } /> */}
            <label className="display-answer__content">
                {`${props.index + 1}. ${props.question.content}`}
            </label>
            <label className="display-answer__content">
                {questionTypeDescription(props.question.questionType)}
            </label>
            <DisplayAnswers answers={props.question.answers} />
        </FormControl>
    );
}