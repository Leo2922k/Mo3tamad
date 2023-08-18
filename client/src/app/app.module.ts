import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ExamDetailComponent } from './exams/exam-detail/exam-detail.component';
import { ExamListComponent } from './exams/exam-list/exam-list.component';
import { AboutComponent } from './about/about.component';
import { CommonModule } from '@angular/common';
import { SharedModule } from './_modules/shared.module';
import { TestErrorComponent } from './errors/test-error/test-error.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { ExamCardComponent } from './exams/exam-card/exam-card.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { ExamQuizComponent } from './exams/exam-quiz/exam-quiz.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';
import { ExamResultComponent } from './exams/exam-result/exam-result.component';
import { LoginComponent } from './login/login.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { HasRoleDirective } from './_directives/has-role.directive';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { ExamManagementComponent } from './admin/exam-management/exam-management.component';
import { ResultManagementComponent } from './admin/result-management/result-management.component';
import { RolesModalComponent } from './modals/roles-modal/roles-modal.component';
import { ExamRepeatComponent } from './exams/exam-repeat/exam-repeat.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { DatePickerComponent } from './_forms/date-picker/date-picker.component';

  

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    ExamDetailComponent,
    ExamListComponent,
    AboutComponent,
    TestErrorComponent,
    NotFoundComponent,
    ServerErrorComponent,
    MemberListComponent,
    ExamCardComponent,
    ExamQuizComponent,
    MemberEditComponent,
    PhotoEditorComponent,
    ExamResultComponent,
    LoginComponent,
    AdminPanelComponent,
    HasRoleDirective,
    UserManagementComponent,
    ExamManagementComponent,
    ResultManagementComponent,
    RolesModalComponent,
    ExamRepeatComponent,
    TextInputComponent,
    DatePickerComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,// for http requset
    FormsModule,
    ReactiveFormsModule,
    SharedModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
