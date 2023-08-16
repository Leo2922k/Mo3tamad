import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { Observable, map, of } from 'rxjs';
import { UserExam } from '../_models/userexam';

@Injectable({
    providedIn: 'root'
  })

  export class UserExamService {

    baseUrl = environment.apiUrl;
  
    userexams: UserExam[] = [];
  
    constructor(private Http: HttpClient) {}
  
      getUsersExams(): Observable<UserExam[]> {
        return this.Http.get<UserExam[]>(`${this.baseUrl}UserExam`);
      }
    
      getUserExam(id: number, examId: number): Observable<UserExam> {
        return this.Http.get<UserExam>(`${this.baseUrl}UserExam/${id}/${examId}`);
      }
    
      /*addUserExamAttempt(attempt: UserExam): Observable<any> {
        return this.http.post(`${this.baseUrl}UserExam/add-attempt`, attempt);
      }*/
      AddUserExamAttemptAsync(attempt: UserExam) {
        return this.Http.post(this.baseUrl + 'UserExam/add-attempt', attempt).toPromise();
      }
    
      updateUserExamAttempt(attempt: UserExam){
        return this.Http.put(`${this.baseUrl}UserExam`, attempt).toPromise();
      }
    
      deleteUserExam(id: number, examId: number) {
        return this.Http.delete(`${this.baseUrl}UserExam/${id}/${examId}`).toPromise();
      }

    
  }