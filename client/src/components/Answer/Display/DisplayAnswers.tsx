import { Stack } from "@mui/material";
import { NewAnswer } from "../../../Model/CreateTest/NewAnswer";
import { DisplayAnswer } from "./DisplayAnswer";

interface DisplayAnswersProps {
    answers: NewAnswer[];
}

export function DisplayAnswers(props: DisplayAnswersProps) {
    const displays = props.answers.map((answer, index) => {
        return <DisplayAnswer key={index} answer={answer} index={index} />;
    });

    return (
        <Stack>{displays}</Stack>
    );
}