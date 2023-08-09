import { Component, OnInit } from '@angular/core';
import { Subscription, interval, take } from 'rxjs';
import { Exams } from 'src/app/_models/exams';
import { ActivatedRoute } from '@angular/router';
import { ExamsService } from 'src/app/_services/exams.service';

@Component({
  selector: 'app-exam-quiz',
  templateUrl: './exam-quiz.component.html',
  styleUrls: ['./exam-quiz.component.css']
})
export class ExamQuizComponent implements OnInit{

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

  constructor(private examsService: ExamsService, private route: ActivatedRoute) { 
  }

  ngOnInit (): void {
    this.loadExam();
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
      this.currentQuestionNo ++;
    } else {
      this.subscription.forEach(element => {
        element.unsubscribe();
      });
      this.finish();
    } 
  }
 

  finish() {
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
      this.correctAnswerCount ++;
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

  startQuiz() {
    this.showWarning = false;
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



}
