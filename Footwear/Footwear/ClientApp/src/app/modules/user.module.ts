import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from '../user/login/login.component';
import { RegisterComponent } from '../user/register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserProfileComponent } from '../user/user-profile/user-profile.component';
import { MaterialModule } from './material.module';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    UserProfileComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule
  ],
  exports: [
    LoginComponent, RegisterComponent, UserProfileComponent
  ]
  
})
export class UserModule { }
