import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  model: any = {}
  loggedIn = false;

  constructor(public accountService: AccountService,
              private router: Router,
              private toastr: ToastrService) {

  }

  ngOnInit(): void {
    this.getCurrentUser();
  }

  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      next: user => this.loggedIn = !!user,
      error: error => this.toastr.error(error.error)
    })
  }

  login() {
    this.accountService.login(this.model).subscribe({

      next: () => {
        this.toastr.success('Welcome mate!');
        this.router.navigateByUrl('/');
      }
    })
    

  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
