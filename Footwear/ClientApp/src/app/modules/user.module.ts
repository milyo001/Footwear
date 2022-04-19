import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from '../user/login/login.component';
import { RegisterComponent } from '../user/register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../modules/material.module';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../interceptors/auth.guard';
import { UserProfileComponent } from '../user/user-profile/user-profile.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'userProfile', component: UserProfileComponent, canActivate: [AuthGuard] }
];

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    UserProfileComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    LoginComponent, RegisterComponent, UserProfileComponent, RouterModule
  ]
  
})
export class UserModule { }
