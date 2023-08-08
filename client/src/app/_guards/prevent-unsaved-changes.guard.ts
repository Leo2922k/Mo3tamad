import { Injectable } from '@angular/core';
import { CanDeactivate, CanDeactivateFn } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

/*export const preventUnsavedChangesGuard: CanDeactivateFn<unknown> = (component, currentRoute, currentState, nextState) => {
  return true;
};*/


@Injectable({
  providedIn: 'root'
})

export class preventUnsavedChangesGuard  {

  canDeactivate(component: MemberEditComponent): boolean {
    if (component.editForm?.dirty) {
      return confirm('Are you sure you want to continue? any unsaved changes will be lost');
    }
    return true;
    }
}