import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Exams } from 'src/app/_models/exams';
import { AdminService } from 'src/app/_services/admin.service';
import { ExamsService } from 'src/app/_services/exams.service';
import { FileUploadService } from 'src/app/file-upload.service.spec';


@Component({
  selector: 'app-exam-management',
  templateUrl: './exam-management.component.html',
  styleUrls: ['./exam-management.component.css']
})
export class ExamManagementComponent {

  exams$: Observable<Exams[]> | undefined;
  showQuestions: boolean[] = [];
  shortLink: string = "";
  loading: boolean = false; // Flag variable
  file: File | any = null;

  constructor(private examsService: ExamsService,
              private adminService: AdminService,
              private http: HttpClient,
              private fileUploadService: FileUploadService) { }

  ngOnInit (): void {
    this.exams$ = this.examsService.getExams();

  }

  toggleQuestions(index: number): void {
    this.showQuestions[index] = !this.showQuestions[index];
  }

  async deleteExam(examid: number) {

    await this.adminService.deleteExam(examid);
    location.reload();
 
 }

    // On file Select
    onChange(event: Event) {
      const target = event.target as HTMLInputElement;
      if (!target.files) return;
      this.file = target.files[0];

  }

  // OnClick of button Upload
  onUpload() {
      this.loading = !this.loading;
      console.log(this.file);
      this.fileUploadService.upload(this.file).subscribe(
          (event: any) => {
              if (typeof (event) === 'object') {

                  // Short link via api response
                  this.shortLink = event.link;

                  this.loading = false; // Flag variable 

                  
              }
          }
      );
  }

}
