import { CheckCircle } from "@mui/icons-material";
import { Checkbox, IconButton, Stack, TextField } from "@mui/material";
import React from "react";
import { NewAnswer } from "../../../Model/CreateTest/NewAnswer";
import { QuestionType } from "../../../Model/CreateTest/NewQuestion";
import CancelIcon from '@mui/icons-material/Cancel';

interface CreateAnswerFormProps {
    questionType: QuestionType;
    onSave: (answer: NewAnswer) => void;
    onCancel: () => void;
}

interface CreateAnswerFormState {
    answer: NewAnswer;
}

export class CreateAnswerForm extends React.Component<CreateAnswerFormProps, CreateAnswerFormState> {
    public constructor(props: CreateAnswerFormProps) {
        super(props);

        this.state = {
            answer: {
                content: "",
                isCorrect: false
            }
        };
    }

    public render(): React.ReactNode {
        return (
            <Stack direction="row" alignItems="center">
                <Checkbox {...{}} onChange={(e, ch) => this.setQuestionCorrect(ch)} />
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
                    onChange={(e) => this.setQuestionContent(e)}
                />
                <IconButton onClick={() => this.props.onSave(this.state.answer)}>
                    <CheckCircle color="success" />
                </IconButton>
                <IconButton onClick={() => this.props.onCancel()}>
                    <CancelIcon color="warning" />
                </IconButton>
            </Stack>
        );
    }

    private setQuestionContent(event: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>): void {
        this.setState((state) => {
            return {
                answer: {
                    ...state.answer,
                    content: event.target.value
                }
            }
        });
    }

    private setQuestionCorrect(checked: boolean): void {
        this.setState((state) => {
            return {
                answer: {
                    ...state.answer,
                    isCorrect: checked
                }
            }
        });
    }
}