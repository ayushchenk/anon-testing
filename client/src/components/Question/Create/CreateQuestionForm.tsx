import { Button, FormControl, FormHelperText, FormLabel, InputLabel, MenuItem, Select, Stack, TextField } from "@mui/material";
import { useFormik } from "formik";
import { QuestionType } from "../../../Model/CreateTest/NewQuestion";
import { NewAnswer } from "../../../Model/CreateTest/NewAnswer";
import { CreateAnswers } from "../../Answer/Create/CreateAnswers";
import validationSchema from "./CreateQuestionForm.validator";
import "./CreateQuestionForm.css";
import initValues, { CreateQuestionFormProps } from "./CreateQuestionForm.types";
import styleProps from "../../Auth/LoginForm/LoginForm.styles";

export function CreateQuestionForm(props: CreateQuestionFormProps) {
    const formik = useFormik({
        initialValues: initValues,
        validationSchema: validationSchema,
        onSubmit: (values) => {
            console.log(values);
            props.onSave(values);
            formik.setSubmitting(false);
        }
    });

    const addAnswer = () => {
        if (formik.values.questionType === QuestionType.String && formik.values.answers.length > 0) {
            return;
        }

        const answers: NewAnswer[] = [...formik.values.answers, {
            content: "",
            isCorrect: false
        }];

        formik.setFieldValue("answers", answers, false);
    };

    const deleteAnswer = (index: number) => {
        const answers = formik.values.answers.filter((_, i) => i !== index);
        formik.setFieldValue("answers", answers, formik.touched.answers && Boolean(formik.errors.answers));
    };

    const setAnswerCorrect = (checked: boolean, index: number) => {
        let answers = [...formik.values.answers];

        if (formik.values.questionType === QuestionType.Single) {
            answers = answers.map<NewAnswer>(a => {
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

        formik.setFieldValue("answers", answers, formik.touched.answers && Boolean(formik.errors.answers));
    }

    const setAnswerContent = (content: string, index: number) => {
        let answers = [...formik.values.answers];

        const updatedAnswer: NewAnswer = {
            ...answers[index],
            content: content
        };

        if (formik.values.questionType == QuestionType.String) {
            updatedAnswer.isCorrect = true;
        }

        answers[index] = updatedAnswer;

        formik.setFieldValue("answers", answers, formik.touched.answers && Boolean(formik.errors.answers));
    }

    return (
        <form onSubmit={formik.handleSubmit} noValidate>
            <FormControl fullWidth={true}>
                <Stack direction="row" justifyContent="space-between" alignItems="center">
                    <FormLabel>New Question</FormLabel>
                    <span>
                        <Button
                            onClick={addAnswer}
                            color="primary">
                            Add Answer
                        </Button>
                        <Button
                            type="submit"
                            color="success">
                            Save
                        </Button>
                        <Button
                            onClick={props.onCancel}
                            color="warning">
                            Cancel
                        </Button>
                    </span>
                </Stack>
                <TextField
                    {...formik.getFieldProps("content")}
                    {...styleProps}
                    label="Question"
                    multiline={true}
                    maxRows={Infinity}
                    placeholder="Content"
                    error={formik.touched.content && Boolean(formik.errors.content)}
                    helperText={formik.touched.content && formik.errors.content}
                />
                <FormControl style={{ marginTop: "15px" }}>
                    <InputLabel>Question Type</InputLabel>
                    <Select
                        {...formik.getFieldProps("questionType")}
                        required
                        variant="outlined"
                        size="small"
                        margin="dense"
                        onChange={e => {
                            formik.setFieldValue("questionType", e.target.value, false);
                            if (e.target.value === QuestionType.String) {
                                formik.setFieldValue("answers", formik.values.answers.slice(0, 1));
                            }
                        }}
                        label="Question Type" >
                        <MenuItem value={QuestionType.Single}>Single Answer</MenuItem>
                        <MenuItem value={QuestionType.Multiple}>Multiple Answer</MenuItem>
                        <MenuItem value={QuestionType.String}>String Answer</MenuItem>
                    </Select>
                </FormControl>
                <CreateAnswers
                    answersCount={formik.values.answers.length}
                    questionType={formik.values.questionType}
                    onDelete={deleteAnswer}
                    onContentChange={setAnswerContent}
                    onCorrectChange={setAnswerCorrect} />
                {
                    formik.touched.answers && Boolean(formik.errors.answers) &&
                    // <label className="validation">{formik.errors.answers}</label>
                    <FormHelperText error>{formik.touched.answers && formik.errors.answers}</FormHelperText>
                }
            </FormControl>
            <hr className="separator" />
        </form >
    );
}