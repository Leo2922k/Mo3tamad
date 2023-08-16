import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { MembersService } from '../_services/members.service';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { User } from '../_models/user';
import { Member } from '../_models/member';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{

  registerMode = false;
  users: any;
  user: User | null = null;
  member: Member | undefined;

  ngOnInit(): void {
    this.loadMember()

  }


  constructor(private accounService: AccountService,
              private memberService: MembersService,
              private toastr: ToastrService) {

                this.accounService.currentUser$.pipe(take(1)).subscribe ({
                  next: user => this.user = user
                })
                
              }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

  
  loadMember() {
    if (!this.user) return;

    this.memberService.getMember(this.user.username).subscribe ({
      next: member => this.member = member
    })
  }

}
