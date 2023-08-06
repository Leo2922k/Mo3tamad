import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { Exams } from 'src/app/_models/exams';

@Component({
  selector: 'app-exam-card',
  templateUrl: './exam-card.component.html',
  styleUrls: ['./exam-card.component.css'],
  //encapsulation: ViewEncapsulation.Emulated
})
export class ExamCardComponent {

  @Input() exam: Exams | undefined;

  ngOnInit(): void {

  }

}
