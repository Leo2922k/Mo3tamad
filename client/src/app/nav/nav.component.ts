import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {

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
        window.location.reload();
        this.router.navigateByUrl('/');
      }
    })
    

  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
