import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Exams } from 'src/app/_models/exams';
import { Member } from 'src/app/_models/member';
import { ExamsService } from 'src/app/_services/exams.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-exam-list',
  templateUrl: './exam-list.component.html',
  styleUrls: ['./exam-list.component.css']
})
export class ExamListComponent {

  exams$: Observable<Exams[]> | undefined;

  constructor(private examsService: ExamsService) { }

  ngOnInit (): void {
    this.exams$ = this.examsService.getExams();
  }
  
}


/** */