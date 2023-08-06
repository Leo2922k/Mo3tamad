import { ProfilePicture } from "./profilepicture";
import { UserExams } from "./userexam";

export interface Member {
    id: number;
    userName: string;
    dateOfBirth: string;
    gender: string;
    profilePictureUrl: string;
    profilePicture: ProfilePicture;
    fullName: string;
    userExams: UserExams[];
}
