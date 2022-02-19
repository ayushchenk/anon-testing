import { Checkbox, Stack, TextField } from "@mui/material";
import { NewAnswer } from "../../../Model/CreateTest/NewAnswer";
import "./DisplayAnswer.css";

interface DisplayAnswerProps {
    answer: NewAnswer;
    index: number;
}

export function DisplayAnswer(props: DisplayAnswerProps) {
    return (
        <Stack direction="row" alignItems="center">
            <Checkbox {...{ readOnly: true }} checked={props.answer.isCorrect} />
            <label className="display-answer__content">
                {`${props.index + 1}. ${props.answer.content}`}
            </label>
        </Stack>
    );
}