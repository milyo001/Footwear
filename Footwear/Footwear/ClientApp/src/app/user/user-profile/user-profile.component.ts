import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { CookieService } from 'ngx-cookie-service';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { IUserData } from '../../interfaces/userData';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements AfterViewInit, OnInit {
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

  constructor(private userService: UserService, private fb: FormBuilder, private toastr: ToastrService) { }
    

  ngOnInit(): void {
    //Initialize user data form validators
    this.setUserFormValidation();
    //Initialize user change password form validators
    this.setPasswordFormValidation();
    //Initialize email form validators
    this.setEmailFormValidation();

  }

  ngAfterViewInit(): void {
      this.loadData();
  }

  setEmailFormValidation(): void {
    this.emailForm = this.fb.group({
      email: ['', [Validators.required, Validators.pattern(this.emailRegex), Validators.maxLength(30)], []],
      confirmEmail: ['', [Validators.required, Validators.pattern(this.emailRegex), Validators.maxLength(30)], []]
    }, { validator: this.matchFields });
  }

  setPasswordFormValidation(): void {
    this.passwordForm = this.fb.group({
      passwords: this.fb.group({
        password: ['', [Validators.required, Validators.maxLength(100), Validators.minLength(6)], []],
        newPassword: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(100)], []],
        confirmPassword: ['', [Validators.required], []]
      }, { validator: this.matchFields })
    });
  }

  setUserFormValidation(): void {
    this.form = this.fb.group({
      email: [],
      firstName: ["", [Validators.required, Validators.maxLength(100)], []],
      lastName: ["", [Validators.required, Validators.maxLength(100)], []],
      phone: ["", [Validators.required, Validators.maxLength(20), Validators.pattern(this.phoneRegex)], []],
      street: ["", [Validators.required, Validators.maxLength(100), Validators.minLength(2)], []],
      state: ["", [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
      country: ["", [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
      city: ["", [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
      zipCode: ["", [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []]
    });
  }

  //Loads already filled data from user if any, otherwise form fields will remain blank
  async loadData(): Promise<void> {
    this.userService.getUserProfile().then(data => {
      this.userData = data as IUserData;
      
      this.form.patchValue({
        firstName: data.firstName,
        lastName: data.lastName,
        phone: data.phone,
        street: data.street,
        state: data.state,
        country: data.country,
        city: data.city,
        zipCode: data.zipCode
      })
      //Set first name and email properties to visualize about the currently logged in user
      this.firstName = this.form.get("firstName").value;
      this.email = data.email;
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

  changePassword(passwordForm: any, passFormDirective: FormGroupDirective): void {
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

  changeEmail(emailForm, emailFormDirective: FormGroupDirective) {
    this.userService.updateEmail(emailForm).subscribe((response: any) => {
      if (response.succeeded) {
        this.toastr.success("Successfully updated your email!");
        this.email = emailForm.email;
        this.emailForm.reset();
        //<mat-error> check the validity of FormGroupDirective,
        //not FormGroup, and resetting FormGroup does not reset FormGroupDirective.
        emailFormDirective.resetForm();
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


