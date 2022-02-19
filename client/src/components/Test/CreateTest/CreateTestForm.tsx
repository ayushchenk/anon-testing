import { Button, Container, Stack } from "@mui/material";
import React from "react";
import { NewQuestion } from "../../../Model/CreateTest/NewQuestion";
import { CreateQuestionForm } from "../Question/CreateQuestionForm";

interface CreateTestFormState {
    questions: NewQuestion[];
    createQuestion: boolean;
}

export class CreateTestForm extends React.Component<{}, CreateTestFormState> {
    public constructor(props: {}) {
        super(props);

        this.state = {
            questions: [],
            createQuestion: false
        };
    }

    public render(): React.ReactNode {
        return (
            <Container maxWidth="md">
                <Button
                    color="success"
                    variant="outlined"
                    onClick={() => this.addQuestionHandler()}>
                    Add Question
                </Button>
                {this.state.createQuestion && <CreateQuestionForm onSave={(q) => this.questionSaveHandler(q)} />}
                {this.state.questions.length}
                <Stack>
                </Stack>
            </Container>
        );
    }

    private addQuestionHandler(): void {
        console.log("add question");
        this.setState({
            createQuestion: true
        });
    }

    private questionSaveHandler(question: NewQuestion): void {
        this.state.questions.push(question);
    }
}