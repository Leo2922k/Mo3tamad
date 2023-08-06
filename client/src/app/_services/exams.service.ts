import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Exams } from '../_models/exams';
import { ExamQuestions } from '../_models/examquestions';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExamsService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getExams() { // 'users': controller name
    return this.http.get<Exams[]>(this.baseUrl + 'exams')
  }

  getExam (examname: string): Observable<Exams> {
    return this.http.get<Exams>(`${this.baseUrl}exams/${examname}`);
  }


  // questions editied
  /*getQuestions() {
    return this.http.get<ExamQuestion[]>(this.baseUrl+'questions')
  }*/
}
