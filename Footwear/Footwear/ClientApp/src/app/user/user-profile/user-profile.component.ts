import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IUserData } from '../../interfaces/userData';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  form: FormGroup; 
  passwordForm: FormGroup;
  emailForm: FormGroup;

  public userData: IUserData = null;

  private phoneRegex: string = '[- +()0-9]+'; 
  public emailSectionToggle = false;
  public passSectionToggle = false;

  constructor(
    private userService: UserService,
    private fb: FormBuilder,
    private toastr: ToastrService
             ) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.userService.getUserProfile().subscribe(data => {
      this.userData = data as IUserData;
      this.form = this.fb.group({
        email: [data.email],
        firstName: [data.firstName, [Validators.required, Validators.maxLength(100)], []],
        lastName: [data.lastName, [Validators.required, Validators.maxLength(100)], []],
        phone: [data.phone, [Validators.required, Validators.maxLength(20), Validators.pattern(this.phoneRegex)], []],
        street: [data.street, [Validators.required, Validators.maxLength(100), Validators.minLength(2)], []],
        state: [data.state, [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
        country: [data.country, [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
        city: [data.city, [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
        zipCode: [data.zipCode, [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []]
      });
      this.passwordForm = this.fb.group({
        passwords: this.fb.group({
          password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(100)], []],
          newPassword: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(100)], []],
          confirmPassword: ['', [Validators.required], []]
        }, { validator: this.confirmPasswords })

      })
    })

  }
  updateProfile(form: any) {
    this.userService.updateUserProfile(form.value).subscribe(
      (response: any) => {
        if (response.succeeded) {
          this.toastr.success("Successfully updated user information!");
          this.loadData();
        }
        else {
          response.errors.forEach(element => {
            this.toastr.error(element.description, 'Update failed!');
          })
        }
      },
      err => {
        console.log(err);
      }
    );
  }

  changePassword(): void {

  }

  changeEmail() {

  }


  //Validate the two password in the form input fields
  confirmPasswords(group: FormGroup) {
    let confirmPassword = group.get('confirmPassword');

    if (confirmPassword.errors == null || 'passwordMismatch' in confirmPassword.errors) {
      if (group.get('password').value != confirmPassword.value) {
        confirmPassword.setErrors({ passwordMismatch: true })
      }
      else {
        confirmPassword.setErrors(null);
      }
    }
  }
  
}


