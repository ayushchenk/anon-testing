import { Alert, Button, Container, FormHelperText, FormLabel, Snackbar, Stack, TextField } from "@mui/material";
import React, { useState } from "react";
import { useFormik } from "formik";
import { NewQuestion } from "../../../Model/CreateTest/NewQuestion";
import { NewTest } from "../../../Model/CreateTest/NewTest";
import { AuthService } from "../../../Services/AuthService";
import styleProps from "../../Auth/LoginForm/LoginForm.styles";
import { CreateQuestionForm } from "../../Question/Create";
import { DisplayQuestions } from "../../Question/Display";
import "./CreateTestForm.css";
import validationSchema from "./CreateTestForm.validator";
import { TestService } from "../../../Services/TestService";
import { useNavigate } from "react-router-dom";

interface CreateTestFormState {
    test: NewTest;
    createQuestion: boolean;
}

export function CreateTest() {
    const navigate = useNavigate();
    const authService = new AuthService();
    const testService = new TestService();
    const [showAlert, setShowAlert] = useState(false);
    const [createQuestion, setCreateQuestion] = useState(false);

    const initValues: NewTest = {
        title: "",
        userId: authService.getToken()?.userId ?? "",
        questions: []
    };

    const formik = useFormik({
        initialValues: initValues,
        validationSchema: validationSchema,
        validateOnBlur: false,
        validateOnChange: false,
        onSubmit: async (values) => {
            console.log(values);

            const response = await testService.create(values);

            if (!response?.ok) {
                setShowAlert(true);
                response?.json().then(console.log);
            }
            else{
                navigate("/");
            }
        }
    });

    return (
        <Container maxWidth="md" className="create-test-form">
            <Snackbar open={showAlert} autoHideDuration={3500} onClose={() => setShowAlert(false)}>
                <Alert severity="warning" sx={{ width: '100%' }} onClose={() => setShowAlert(false)}>
                    Error while creating test
                </Alert>
            </Snackbar>
            <Stack direction="row" justifyContent="space-between" alignItems="center">
                <FormLabel>New Test</FormLabel>
                <span>
                    <Button
                        color="primary"
                        onClick={() => setCreateQuestion(true)}>
                        Add Question
                    </Button>
                    <Button
                        color="success"
                        onClick={() => formik.submitForm()}>
                        Save Test
                    </Button>
                </span>
            </Stack>
            <TextField
                {...formik.getFieldProps("title")}
                {...styleProps}
                label="Title"
                multiline={true}
                maxRows={Infinity}
                placeholder="Title"
                fullWidth={true}
                error={formik.touched.title && Boolean(formik.errors.title)}
                helperText={formik.touched.title && formik.errors.title}
            />
            <hr className="separator" />
            {
                createQuestion &&
                <CreateQuestionForm
                    onSave={(q) => {
                        formik.setFieldValue("questions", [...formik.values.questions, q], true);
                        setCreateQuestion(false);
                    }}
                    onCancel={() => setCreateQuestion(false)}
                />
            }
            <FormHelperText error>{formik.errors.questions}</FormHelperText>
            <DisplayQuestions questions={formik.values.questions}></DisplayQuestions>
        </Container>
    );
}

export class CreateTestForm extends React.Component<{}, CreateTestFormState> {
    private readonly authService = new AuthService();

    public constructor(props: {}) {
        super(props);

        this.state = {
            test: {
                title: "",
                questions: [],
                userId: this.authService.getToken()?.userId ?? ""
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
                    {...styleProps}
                    label="Title"
                    multiline={true}
                    maxRows={Infinity}
                    placeholder="Title"
                    fullWidth={true}
                    onChange={(e) => this.setTestTitle(e)} />
                <hr className="separator" />
                {
                    this.state.createQuestion &&
                    <CreateQuestionForm
                        onSave={(q) => this.saveQuestion(q)}
                        onCancel={() => this.cancelQuestion()}
                    />
                }
                <DisplayQuestions questions={this.state.test.questions}></DisplayQuestions>
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
        console.log(this.state.test);
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