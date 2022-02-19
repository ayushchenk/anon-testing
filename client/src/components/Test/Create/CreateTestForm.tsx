import { Button, Container, FormLabel, Stack, TextField } from "@mui/material";
import React from "react";
import { NewQuestion } from "../../../Model/CreateTest/NewQuestion";
import { NewTest } from "../../../Model/CreateTest/NewTest";
import { AuthService } from "../../../Services/AuthService";
import { CreateQuestionForm } from "../../Question/Create/CreateQuestionForm";
import { DisplayQuestions } from "../../Question/Display/DisplayQuestions";
import "./CreateTestForm.css";

interface CreateTestFormState {
    test: NewTest;
    createQuestion: boolean;
}

export class CreateTestForm extends React.Component<{}, CreateTestFormState> {
    private readonly authService = new AuthService();

    public constructor(props: {}) {
        super(props);

        this.state = {
            test: {
                title: "",
                questions: [],
                userId: this.authService.getToken()!.userId
            },
            createQuestion: false
        };
    }

    public render(): React.ReactNode {
        return (
            <Container maxWidth="md" className="create-test-form">
                <Stack direction="row" justifyContent="space-between" alignItems="center">
                    <FormLabel>New Test</FormLabel>
                    <span>
                        <Button
                            color="primary"
                            onClick={() => this.createQuestion()}>
                            Add Question
                        </Button>
                        <Button
                            color="success"
                            onClick={() => this.save()}>
                            Save Test
                        </Button>
                    </span>
                </Stack>
                <TextField
                    variant="outlined"
                    size="small"
                    label="Title"
                    required
                    margin="dense"
                    multiline={true}
                    maxRows={Infinity}
                    placeholder="Title"
                    fullWidth={true}
                    onChange={(e) => this.setTestTitle(e)} />
                <hr />
                {
                    this.state.createQuestion &&
                    <CreateQuestionForm
                        onSave={(q) => this.saveQuestion(q)}
                        onCancel={() => this.cancelQuestion()}
                    />
                }
                <DisplayQuestions questions={this.state.test.questions} />
            </Container>
        );
    }

    private createQuestion(): void {
        this.setState({
            createQuestion: true
        });
    }

    private saveQuestion(question: NewQuestion): void {
        this.state.test.questions.push(question);
        this.setState({
            createQuestion: false
        });
    }

    private cancelQuestion(): void {
        this.setState({
            createQuestion: false
        });
    }

    private save(): void {

    }

    private setTestTitle(event: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>): void {
        this.setState(state => {
            return {
                test: {
                    ...state.test,
                    title: event.target.value
                }
            }
        });
    }
}