import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
  public firstName: string;
  private phoneRegex: string = '[- +()0-9]+';
  private emailRegex: string = '[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,63}$';
  public emailSectionToggle: boolean = false;
  public passSectionToggle: boolean = false;

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
      //Set first name to 'greet the user'
      this.firstName = this.form.get("firstName").value;
      this.passwordForm = this.fb.group({
        passwords: this.fb.group({
          password: ['', [Validators.required, Validators.maxLength(100), Validators.minLength(6)], []],
          newPassword: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(100)], []],
          confirmPassword: ['', [Validators.required], []]
        }, { validator: this.confirmPasswords })
      });
      this.emailForm = this.fb.group({
        email: ['', [Validators.required, Validators.pattern(this.emailRegex), Validators.maxLength(30)], []],
        confirmEmail: ['', [Validators.required, Validators.pattern(this.emailRegex), Validators.maxLength(30)], []]
      }, { validator: this.confirmEmails });
    })

  }
  updateProfile(form: any) {
    this.userService.updateUserProfile(form.value).subscribe(
      (response: any) => {
        if (response.succeeded) {
          this.toastr.success("Successfully updated user information!");
          this.loadData();
        }
      },
      error => {
        console.log(error);
        this.toastr.error(error.error.message);
      }
    );
  }

  changePassword(passwordForm) {
    this.userService.updatePassword(passwordForm).subscribe((response: any) => {
      if (response.succeeded) {
        this.toastr.success("Successfully updated your password!");
      }
    },
      error => {
        this.toastr.error(error.error.message);
      });
  }

  changeEmail(emailForm) {
    this.userService.updateEmail(emailForm).subscribe((response: any) => {
      if (response.succeeded) {
        this.toastr.success("Successfully updated your email!");
      }
    },
      err => {
        this.toastr.error(err.error.message);
      })
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

  confirmEmails(group: FormGroup) {
    let confirmEmail = group.get('confirmEmail');

    if (confirmEmail.errors == null || 'emailMismatch' in confirmEmail.errors) {
      if (group.get('email').value != confirmEmail.value) {
        confirmEmail.setErrors({ emailMismatch: true })
      }
      else {
        confirmEmail.setErrors(null);
      }
    }
  }

}


