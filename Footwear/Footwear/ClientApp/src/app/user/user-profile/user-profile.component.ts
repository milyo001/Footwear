import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { IUserData } from '../../interfaces/userData';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent  {
  form: FormGroup;
  passwordForm: FormGroup;
  emailForm: FormGroup;

  public firstName: string;
  public email: string;
  public userData: IUserData = null;
 
  private phoneRegex: string = '[- +()0-9]+';
  private emailRegex: string = '[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,63}$';
  public emailSectionToggle: boolean = false;
  public passSectionToggle: boolean = false;

  constructor(
    private userService: UserService,
    private fb: FormBuilder,
    private toastr: ToastrService
  ) {
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
      //Set first name and email to show the currently logged in user
      this.firstName = this.form.get("firstName").value;
      this.email = this.form.get("email").value;
      //Set validaions for password form
      this.passwordForm = this.fb.group({
        passwords: this.fb.group({
          password: ['', [Validators.required, Validators.maxLength(100), Validators.minLength(6)], []],
          newPassword: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(100)], []],
          confirmPassword: ['', [Validators.required], []]
        }, { validator: this.matchFields })
      });
      //Set validaions for email form
      this.emailForm = this.fb.group({
        email: ['', [Validators.required, Validators.pattern(this.emailRegex), Validators.maxLength(30)], []],
        confirmEmail: ['', [Validators.required, Validators.pattern(this.emailRegex), Validators.maxLength(30)], []]
      }, { validator: this.matchFields });
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
        this.toastr.error(error.error.message);
      }
    );
  }

  changePassword(passwordForm: any, passFormDirective: FormGroupDirective) : void {
    this.userService.updatePassword(passwordForm.passwords).subscribe((response: any) => {
      if (response.succeeded) {
        this.toastr.success("Successfully updated your password!");
        this.passwordForm.reset();
        //<mat-error> check the validity of FormGroupDirective,
        //not FormGroup, and resetting FormGroup does not reset FormGroupDirective.
        passFormDirective.resetForm();
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
        this.email = emailForm.email;
      }
    },
      err => {
        this.toastr.error(err.error.message);
      })
  }

  //Check if password or email are matching the confirm email/passcode fields
  matchFields(group: FormGroup) {
    //Check if password and confirmPassword match
    if (group.contains('confirmPassword')) {
      let confirmPassword = group.get('confirmPassword');
      if (confirmPassword.errors == null || 'passwordMismatch' in confirmPassword.errors) {
        if (group.get('newPassword').value != confirmPassword.value) {
          confirmPassword.setErrors({ passwordMismatch: true })
        }
        else {
          confirmPassword.setErrors(null);
        }
      }
    }
    //Check if email and confirmEmail match
    else if (group.contains('confirmEmail')) {
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

}


