import { Component, Input, OnInit } from '@angular/core';
import { NotFoundError, Observable, Subscription, catchError, interval, take, tap } from 'rxjs';
import { Exams } from 'src/app/_models/exams';
import { ActivatedRoute } from '@angular/router';
import { ExamsService } from 'src/app/_services/exams.service';
import { UserExamService } from 'src/app/_services/userexam.service';
import { UserExam } from 'src/app/_models/userexam';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-exam-quiz',
  templateUrl: './exam-quiz.component.html',
  styleUrls: ['./exam-quiz.component.css']
})
export class ExamQuizComponent implements OnInit{

  member: Member | undefined;
  user: User | null = null;

  
  exam: Exams | undefined;
  showWarning: boolean = false;
  questionsList: any[]= [];
  currentQuestionNo: number = 0;
  isQuizStarted: boolean = false;
  isQuizEnded: boolean = false;
  remainingTime:number = 15;
  timer = interval(1000);
  subscription: Subscription [] = [];
  correctAnswerCount: number = 0;
  userExamSubscription: Subscription | undefined;


  userexamattempt$: Observable<UserExam> | undefined;



  constructor(private examsService: ExamsService, private route: ActivatedRoute
             ,private userExamService: UserExamService, private accounService: AccountService, private memberService: MembersService) {
              
              this.accounService.currentUser$.pipe(take(1)).subscribe ({
                next: user => this.user = user
              })

  }

  ngOnInit (): void {
    this.loadMember();
    this.loadExam();
  }

  loadMember() {
    if (!this.user) return;

    this.memberService.getMember(this.user.username).subscribe ({
      next: member => this.member = member
    })
  }

  getLoadQuestions() {
    if(!this.exam) return [];

    const questionslist = [];

    for (const question of this.exam.examQuestion) {
      questionslist.push(question)
    }

    return questionslist;
  }

  
  loadExam () {
    const examname = this.route.snapshot.paramMap.get('examname');
    if (!examname) return;
    this.examsService.getExam(examname).subscribe({
      next: exam => {
        this.exam = exam;
        this.questionsList = this.getLoadQuestions();
      }
    })
  }

  nextQuestion() {
    this.remainingTime = 15;
    if(this.currentQuestionNo < this.questionsList.length-1) {
      this.currentQuestionNo++;
    } else {
      this.finish();
      this.subscription.forEach(element => {
        element.unsubscribe();
      });
    } 
  }
 

  async finish() {
    console.log("submitted")
    await this.submitUserExamAttempt();
    console.log("finished")
    this.isQuizEnded = true;
    this.isQuizStarted = false; 
    this.currentQuestionNo = 0;
  }

  start() {
    this.showWarning = false;
    this.isQuizEnded = false;
    this.isQuizStarted = false;  
  }

  showWarningPopup() {
    this.showWarning = true;
  }

  selectOption(answer: any) {
    if(answer.answersTrue) {
      this.correctAnswerCount++;
    }
    answer.isSelected = true;
  }

  isOptionSelected(ExamQuestions: any) {
    const selectionCount = ExamQuestions.filter((m:any)=>m.isSelected == true).length;
    if(selectionCount == 0) {
      return false;
    } else {
      return true;
    }
  }

  async startQuiz() {
    this.showWarning = true;
    this.isQuizStarted = true;  
    this.subscription.push(this.timer.subscribe(res=> {
      console.log(res);
      if(this.remainingTime != 0) {
        this.remainingTime --;
      } 
      if(this.remainingTime == 0) {
        this.nextQuestion();
      } 
    })
   )
  }



  
  async submitUserExamAttempt() {

    if (!this.exam) return;
    if (!this.member) return;

 
    const userExamAttempty: UserExam = {
      id: this.member?.id,
      examId: this.exam?.examId,
      userExamScreenVideoUrl: 'sample-screen-video-url',
      userExamCamVideoUrl: 'sample-cam-video-url',
      userExamGrade: (this.correctAnswerCount)
    };

   await this.userExamService.AddUserExamAttemptAsync(userExamAttempty);

  // await this.userExamService.updateUserExamAttempt(userExamAttempty);
   //await this.userExamService.deleteUserExam(5,2);
   
}

/*async redoUserExamAttempt() {

  if (!this.exam) return;
  if (!this.member) return;


  const userExamAttempty: UserExam = {
    id: this.member?.id,
    examId: this.exam?.examId,
    userExamScreenVideoUrl: 'sample-screen-video-url',
    userExamCamVideoUrl: 'sample-cam-video-url',
    userExamGrade: (this.correctAnswerCount)
  };

 //await this.userExamService.AddUserExamAttemptAsync(userExamAttempty);

 await this.userExamService.updateUserExamAttempt(userExamAttempty);
 //await this.userExamService.deleteUserExam(5,2);
 
}*/

 /*async previousAttempt() {

  if (!this.exam) return;
  if (!this.member) return;

  if (this.userExamService.getUserExam(this.member?.id , this.exam?.examId)) {
    await this.startQuiz();
    await this.submitUserExamAttempt();
  }
  else {
    await this.startQuiz();
    this.redoUserExamAttempt();
  }

}*/
}