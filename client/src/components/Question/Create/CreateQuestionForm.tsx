import { Button, FormControl, FormLabel, MenuItem, Select, SelectChangeEvent, Stack, TextField } from "@mui/material";
import React from "react";
import { NewAnswer } from "../../../Model/CreateTest/NewAnswer";
import { NewQuestion, QuestionType } from "../../../Model/CreateTest/NewQuestion";
import { CreateAnswerForm } from "../../Answer/Create/CreateAnswerForm";
import { DisplayAnswers } from "../../Answer/Display/DisplayAnswers";

export interface CreateQuestionFormProps {
    onSave: (question: NewQuestion) => void;
    onCancel: () => void;
}

interface CreateQuestionFormState {
    question: NewQuestion;
    createAnswer: boolean;
}

export class CreateQuestionForm extends React.Component<CreateQuestionFormProps, CreateQuestionFormState> {
    public constructor(props: CreateQuestionFormProps) {
        super(props);

        this.state = {
            createAnswer: false,
            question: {
                content: "",
                questionType: QuestionType.Single,
                answers: []
            }
        };
    }

    public render(): React.ReactNode {
        return (
            <>
                <FormControl fullWidth={true}>
                    <Stack direction="row" justifyContent="space-between" alignItems="center">
                        <FormLabel>New Question</FormLabel>
                        <span>
                            <Button
                                onClick={() => this.addAnswer()}
                                color="primary">
                                Add Answer
                            </Button>
                            <Button
                                onClick={() => this.save()}
                                color="success">
                                Save
                            </Button>
                            <Button
                                onClick={() => this.props.onCancel()}
                                color="warning">
                                Cancel
                            </Button>
                        </span>
                    </Stack>
                    <TextField
                        variant="outlined"
                        size="small"
                        label="Question"
                        required
                        margin="dense"
                        multiline={true}
                        maxRows={Infinity}
                        placeholder="Content"
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
                    <DisplayAnswers answers={this.state.question.answers} />
                    {
                        this.state.createAnswer &&
                        <CreateAnswerForm
                            questionType={this.state.question.questionType}
                            onSave={(a) => this.saveAnswer(a)}
                            onCancel={() => this.cancelAnswer()} />
                    }
                </FormControl>
                <hr />
            </>
        );
    }

    private save(): void {
        this.props.onSave(this.state.question);
    }

    private addAnswer(): void {
        this.setState({
            createAnswer: true
        });
    }

    private saveAnswer(answer: NewAnswer): void {
        this.state.question.answers.push(answer);
        this.setState({
            createAnswer: false
        });
    }

    private cancelAnswer(): void {
        this.setState({
            createAnswer: false
        });
    }

    private setQuestionType(event: SelectChangeEvent<QuestionType>): void {
        this.setState((state) => {
            return {
                question: {
                    ...state.question,
                    questionType: event.target.value as QuestionType
                }
            }
        });
    }

    private setQuestionContent(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>): void {
        this.setState((state) => {
            return {
                question: {
                    ...state.question,
                    content: event.target.value
                }
            }
        });
    }
}