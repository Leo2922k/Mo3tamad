import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Exams } from '../_models/exams';
import { ExamQuestions } from '../_models/examquestions';
import { Observable, map, of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExamsService {

  baseUrl = environment.apiUrl;

  exams: Exams[] = [];

  constructor(private http: HttpClient) {}

  getExams() { // 'users': controller name
    if (this.exams.length > 0) return of(this.exams);
    return this.http.get<Exams[]>(this.baseUrl + 'exams').pipe (
      map(exams => {
        this.exams = exams;
        return exams;
      })
    )
  }

  getExam (examname: string): Observable<Exams> {
    const exam = this.exams.find (x => x.examName === examname);
    if (exam) return of(exam);
    return this.http.get<Exams>(`${this.baseUrl}exams/${examname}`);
  }

  /*getExambyid (examid: number): Observable<string> {
    const exam = this.exams.find (x => x.examId === examid);
    if (exam) return of(exam);
    return this.http.get<string>(`${this.baseUrl}exams/${examid}`);
  }*/


  // questions editied
  /*getQuestions() {
    return this.http.get<ExamQuestion[]>(this.baseUrl+'questions')
  }*/
}
