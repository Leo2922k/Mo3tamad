import { Exams } from "./exams";
import { Member } from "./member";

export interface UserExam {
    userExamScreenVideoUrl: string;
    userExamCamVideoUrl: string;
    userExamGrade: number;
    id: number;
    examId: number;
}