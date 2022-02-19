import { Button, FormControl, FormLabel, MenuItem, Select, SelectChangeEvent, TextField } from "@mui/material";
import React from "react";
import { NewQuestion, QuestionType } from "../../../Model/CreateTest/NewQuestion";

export interface CreateQuestionFormProps {
    onSave: (question: NewQuestion) => void;
}

interface CreateQuestionFormState {
    question: NewQuestion;
}

export class CreateQuestionForm extends React.Component<CreateQuestionFormProps, CreateQuestionFormState> {
    public constructor(props: CreateQuestionFormProps) {
        super(props);

        this.state = {
            question: {
                content: "",
                questionType: QuestionType.Single,
                answers: []
            }
        };
    }

    public render(): React.ReactNode {
        return (
            <FormControl fullWidth={true}>
                <FormLabel>New Question</FormLabel>
                <TextField
                    variant="outlined"
                    size="small"
                    required
                    label="Question"
                    multiline={true}
                    maxRows={Infinity}
                    placeholder="Content"
                    margin="dense"
                    onChange={(e) => this.setQuestionContent(e)} />
                <Select
                    required
                    size="small"
                    value={this.state.question.questionType}
                    margin="dense"
                    onChange={(e) => this.setQuestionType(e)}>
                    <MenuItem value={QuestionType.Single}>Single Answer</MenuItem>
                    <MenuItem value={QuestionType.Multiple}>Multiple Answer</MenuItem>
                    <MenuItem value={QuestionType.String}>String Answer</MenuItem>
                </Select>
                <Button
                    onClick={() => this.save()} >
                    Save
                </Button>
            </FormControl>
        );
    }

    private save(): void {
        this.props.onSave(this.state.question);
    }

    private setQuestionType(event: SelectChangeEvent<QuestionType>): void {
        this.setState({
            question: {
                ...this.state.question,
                questionType: event.target.value as QuestionType
            }
        });
    }

    private setQuestionContent(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>): void {
        this.setState({
            question: {
                ...this.state.question,
                content: event.target.value
            }
        });
    }
}