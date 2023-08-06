import { ExamPicture } from "./exampicture";
import { ExamQuestions } from "./examquestions";
import { UserExams } from "./userexam";

export interface Exams {
    examId: number;
    examName: string;
    examDescription: string;
    examGrade: number;
    examPictureUrl: string;
    examPicture: ExamPicture;
    examQuestion: ExamQuestions[]
    userExams: UserExams[];
}

