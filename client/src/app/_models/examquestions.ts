import { QuestionAnswer } from "./questionanswer";


export interface ExamQuestions {
    questionId: number;
    questionText: string;
    questionMark: number;
    questionAnswers: QuestionAnswer[];
}
