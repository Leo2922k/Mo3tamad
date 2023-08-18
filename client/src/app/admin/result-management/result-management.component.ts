import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Observable, take } from 'rxjs';
import { Exams } from 'src/app/_models/exams';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { UserExam } from 'src/app/_models/userexam';
import { AccountService } from 'src/app/_services/account.service';
import { ExamsService } from 'src/app/_services/exams.service';
import { MembersService } from 'src/app/_services/members.service';
import { UserExamService } from 'src/app/_services/userexam.service';

@Component({
  selector: 'app-result-management',
  templateUrl: './result-management.component.html',
  styleUrls: ['./result-management.component.css']
})
export class ResultManagementComponent {

  userexams$: Observable<UserExam[]> | undefined;
  exams$: Observable<Exams[]> | undefined;
  user: User | null = null;
  members$: Observable<Member[]>  | undefined;
  showAttempts: boolean[] = [];

  constructor(
    private userexamService: UserExamService,
    private examsService: ExamsService,
    private accountService: AccountService,
    private http: HttpClient,
    private memberService: MembersService
   ) { }

  ngOnInit (): void {
    this.userexams$ = this.userexamService.getUsersExams();
    this.exams$ = this.examsService.getExams();
    this.members$ = this.memberService.getMembers();

    this.accountService.currentUser$.pipe(take(1)).subscribe ({
      next: user => this.user = user
    })
  }

  
  async delete(id: number, examid: number) {

   await this.userexamService.deleteUserExam(id,examid);
   location.reload();

}

toggleAttempts(index: number): void {
    this.showAttempts[index] = !this.showAttempts[index];
  }

}
