import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import { environment } from 'src/environments/environment';

  @Injectable({
  providedIn: 'root'
  })

  export class FileUploadService {
    
  // API url
  baseApiUrl = environment.apiUrl;
    
  constructor(private http:HttpClient) { }

  // Returns an observable
  upload(file: File):Observable<any> {
    return this.http.post(this.baseApiUrl +'admin/add-exam', file)
  }
}
