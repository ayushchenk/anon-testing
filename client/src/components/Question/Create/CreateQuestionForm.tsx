import { Button, FormControl, FormLabel, MenuItem, Select, SelectChangeEvent, Stack, TextField } from "@mui/material";
import React from "react";
import { NewQuestion, QuestionType } from "../../../Model/CreateTest/NewQuestion";
import { NewAnswer } from "../../../Model/CreateTest/NewAnswer";
import { CreateAnswers } from "../../Answer/Create/CreateAnswers";
import { QuestionValidator } from "./CreateQuestionForm.validator";
import "./CreateQuestionForm.css";

export interface CreateQuestionFormProps {
    onSave: (question: NewQuestion) => void;
    onCancel: () => void;
}

interface CreateQuestionFormState {
    question: NewQuestion;
    contentValidation?: string;
    answersValidation?: string;
    validationEnabled: boolean;
}

export class CreateQuestionForm extends React.Component<CreateQuestionFormProps, CreateQuestionFormState> {
    private readonly validator = new QuestionValidator();

    public constructor(props: CreateQuestionFormProps) {
        super(props);

        this.state = {
            validationEnabled: false,
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
                        error={this.state.contentValidation !== undefined && this.state.validationEnabled}
                        helperText={this.state.validationEnabled ? this.state.contentValidation : ""}
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
                    <CreateAnswers
                        answersCount={this.state.question.answers.length}
                        questionType={this.state.question.questionType}
                        onDelete={(i) => this.deleteAnswer(i)}
                        onContentChange={(content, i) => this.setAnswerContent(content, i)}
                        onCorrectChange={(checked, i) => this.setAnswerCorrect(checked, i)} />
                    {
                        this.state.validationEnabled && this.state.answersValidation !== undefined &&
                        <label className="validation">{this.state.answersValidation}</label>
                    }
                </FormControl>
                <hr className="separator"/>
            </>
        );
    }

    private save(): void {
        this.setState({
            validationEnabled: true
        }, () => {
            if (this.validateInput()) {
                this.props.onSave(this.state.question);
            }
        });
    }

    private validateInput(): boolean {
        if (this.state.validationEnabled) {
            const result = this.validator.validate(this.state.question);

            this.setState({
                contentValidation: result.get("content"),
                answersValidation: result.get("answers")
            });

            return result.size === 0;
        }

        return false;
    }

    private addAnswer(): void {
        this.setState((prevState) => {
            return {
                question: {
                    ...prevState.question,
                    answers: [...prevState.question.answers, {
                        content: "",
                        isCorrect: false
                    }]
                }
            };
        }, () => this.validateInput());
    }

    private deleteAnswer(index: number): void {
        this.setState((state) => {
            return {
                question: {
                    ...state.question,
                    answers: state.question.answers.filter((_, i) => i !== index)
                }
            };
        }, () => this.validateInput());
    }

    private setAnswerContent(content: string, index: number): void {
        const answers = [...this.state.question.answers];

        const updatedAnswer: NewAnswer = {
            ...answers[index],
            content: content
        };

        answers[index] = updatedAnswer;

        this.setState((prevState) => {
            return {
                question: {
                    ...prevState.question,
                    answers: answers
                }
            }
        }, () => this.validateInput());
    }

    private setAnswerCorrect(checked: boolean, index: number): void {
        let answers = [...this.state.question.answers];

        if (this.state.question.questionType === QuestionType.Single) {
            answers = answers.map(a => {
                return {
                    ...a,
                    isCorrect: false
                }
            });
        }

        const updatedAnswer: NewAnswer = {
            ...answers[index],
            isCorrect: checked
        };

        answers[index] = updatedAnswer;

        this.setState((prevState) => {
            return {
                question: {
                    ...prevState.question,
                    answers: answers
                }
            }
        }, () => this.validateInput());
    }

    private setQuestionType(event: SelectChangeEvent<QuestionType>): void {
        const answers = this.state.question.answers.map(a => {
            return {
                ...a,
                isCorrect: false
            }
        });

        this.setState((state) => {
            return {
                question: {
                    ...state.question,
                    answers: answers,
                    questionType: event.target.value as QuestionType
                }
            }
        }, () => this.validateInput());
    }

    private setQuestionContent(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>): void {
        this.setState((state) => {
            return {
                question: {
                    ...state.question,
                    content: event.target.value
                }
            }
        }, () => this.validateInput());
    }
}