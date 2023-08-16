import { Injectable } from '@angular/core';
import { CanActivateFn , CanActivate} from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AdminGuard {

  constructor(private accountService: AccountService, private toastr: ToastrService) {}

  canActivate(): Observable<boolean> {
    return this.accountService.currentUser$.pipe (
      map(user => {
        if (!user) return false;

        if (user.roles.includes('Admin') || user.roles.includes('Moderator')) {
          return true;
        } else {
          this.toastr.error('You shall not pass!');
          return false;
        }
      })
    )
  }
}
