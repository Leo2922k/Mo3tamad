import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ExamQuestions } from 'src/app/_models/examquestions';
import { Exams } from 'src/app/_models/exams';
import { QuestionAnswer } from 'src/app/_models/questionanswer';
import { ExamsService } from 'src/app/_services/exams.service';

@Component({
  selector: 'app-exam-detail',
  templateUrl: './exam-detail.component.html',
  styleUrls: ['./exam-detail.component.css'],
})
export class ExamDetailComponent {

  exam: Exams | undefined;
  questions: ExamQuestions[] = [];

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
        this.questions = this.getLoadQuestions();
      }
    })
  }
}



