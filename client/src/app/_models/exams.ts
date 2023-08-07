import { ExamPicture } from "./exampicture";
import { ExamQuestions } from "./examquestions";
import { UserExam } from "./userexam";

export interface Exams {
    examId: number;
    examName: string;
    examDescription: string;
    examGrade: number;
    examPictureUrl: string;
    examPicture: ExamPicture;
    examQuestion: ExamQuestions[]
    userExams: UserExam[];
}

