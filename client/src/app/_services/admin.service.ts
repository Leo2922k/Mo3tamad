import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { Exams } from '../_models/exams';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getUsersWithRoles() {
    return this.http.get<User[]> (this.baseUrl + 'admin/users-with-roles');
  }

  updateUserRoles(username: string, roles: string[]) {
    return this.http.post<string[]>(this.baseUrl +'admin/edit-roles/'
                                   + username + '?roles=' + roles,{});
  }

  deleteExam(examId: number) {
    return this.http.delete(`${this.baseUrl}admin/deleteexam/${examId}`).toPromise();
  }


  AddExam(file: File): Promise<File> {

    return this.http.post<any>(this.baseUrl +'admin/add-exam', file).toPromise();
  }
}
