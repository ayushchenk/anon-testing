import { Checkbox, IconButton, Radio, RadioGroup, Stack, TextField } from "@mui/material";
import React from "react";
import { QuestionType } from "../../../Model/CreateTest/NewQuestion";
import DeleteIcon from '@mui/icons-material/Delete';

interface CreateAnswersProps {
    answersCount: number;
    questionType: QuestionType;
    onContentChange: (content: string, index: number) => void;
    onCorrectChange: (checked: boolean, index: number) => void;
    onDelete: (index: number) => void;
}

export class CreateAnswers extends React.Component<CreateAnswersProps> {
    public constructor(props: CreateAnswersProps) {
        super(props);
    }

    public render(): React.ReactNode {
        const answers = Array.from(Array(this.props.answersCount).keys()).map(i => {
            return (
                <Stack direction="row" alignItems="center" key={i}>
                    {
                        this.props.questionType === QuestionType.Single &&
                        <Radio {...{}} onChange={(_, ch) => this.props.onCorrectChange(ch, i)} value={i} />
                    }
                    {
                        this.props.questionType === QuestionType.Multiple &&
                        <Checkbox {...{}} onChange={(_, ch) => this.props.onCorrectChange(ch, i)} />
                    }
                    <TextField
                        variant="outlined"
                        label="Answer"
                        placeholder="Content"
                        size="small"
                        required
                        fullWidth={true}
                        multiline={true}
                        maxRows={Infinity}
                        margin="dense"
                        onChange={(e) => this.props.onContentChange(e.target.value, i)}
                    />
                    <IconButton onClick={() => this.props.onDelete(i)}>
                        <DeleteIcon />
                    </IconButton>
                </Stack>
            );
        });

        return (
            <RadioGroup style={{ marginTop: answers.length > 0 ? "15px" : "0" }}>
                {answers}
            </RadioGroup>
        );
    }
}