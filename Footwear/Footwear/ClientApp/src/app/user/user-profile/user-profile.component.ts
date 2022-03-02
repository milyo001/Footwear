import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { validateNewAndConfPass, validateOldAndNewPass, validateEmails } from '../../../shared/validators/user-profile.validators';
import { IEmailData } from '../../interfaces/user/emailData';
import { IPasswordData } from '../../interfaces/user/passwordData';
import { IUserData } from '../../interfaces/user/userData';
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
  };

  ngAfterViewInit(): void {
     this.loadDataAsync();
  };

  setEmailFormValidation(): void {
    this.emailForm = this.fb.group({
      email: ['', [Validators.required, Validators.pattern(this.emailRegex), Validators.maxLength(30)], []],
      confirmEmail:
        ['', [Validators.required, Validators.pattern(this.emailRegex), Validators.maxLength(30),
          validateEmails], []]
    });
  };

  setPasswordFormValidation(): void {
    this.passwordForm = this.fb.group({
      passwords: this.fb.group({
        password: new FormControl('', [Validators.required, Validators.maxLength(100), Validators.minLength(6)]),
        newPassword: new FormControl('',
          [
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(100),
            validateOldAndNewPass
          ]
        ),
        confirmPassword: new FormControl('', [Validators.required, validateNewAndConfPass])
      })
    });
  };

  setUserFormValidation(): void {
    this.form = this.fb.group({
      firstName: ["", [Validators.required, Validators.maxLength(100)], []],
      lastName: ["", [Validators.required, Validators.maxLength(100)], []],
      phone: ["", [Validators.required, Validators.maxLength(20), Validators.pattern(this.phoneRegex)], []],
      street: ["", [Validators.required, Validators.maxLength(100), Validators.minLength(2)], []],
      state: ["", [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
      country: ["", [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
      city: ["", [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
      zipCode: ["", [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []]
    });
  };

  //Loads already filled data from user if any, otherwise form fields will remain blank
    async loadDataAsync(): Promise<void> {
      await this.userService.getUserProfile()
        .then(data => {
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
          });
          //Set first name and email properties in component to visualize the currently logged in user
          this.firstName = this.form.get("firstName").value;
          this.email = data.email;
        });

  }

  updateProfile(form: any) {
    const userData: IUserData = form.value;
    this.userService.updateUserProfile(userData).subscribe(
      (response: any) => {
        if (response.succeeded) {
          this.toastr.success("Successfully updated user information!");
          this.loadDataAsync();
        }
      },
      error => {
        this.toastr.error(error.error.message);
        console.log(error.error.message);
      }
    );
  }

  changePassword(passwordForm: any, passFormDirective: FormGroupDirective): void {
    const passwordData: IPasswordData = passwordForm.passwords;
    this.userService.updatePassword(passwordData).subscribe((response: any) => {
      if (response.succeeded) {
        this.toastr.success("Successfully updated your password!");
        this.passwordForm.reset();
        //<mat-error> check the validity of FormGroupDirective,
        //not FormGroup and resetting FormGroup does not reset FormGroupDirective.
        passFormDirective.resetForm();
      }
    },
      error => {
        this.toastr.error(error.error.message);
      });
  }

  changeEmail(emailForm, emailFormDirective: FormGroupDirective) {
    const emailData: IEmailData = emailForm;
    this.userService.updateEmail(emailData).subscribe((response: any) => {
      if (response.succeeded) {
        this.toastr.success("Successfully updated your email!");
        this.email = emailForm.email;
        this.emailForm.reset();
        //<mat-error> check the validity of FormGroupDirective,
        //not FormGroup and resetting FormGroup does not reset FormGroupDirective.
        emailFormDirective.resetForm();
      }
    },
      err => {
        this.toastr.error(err.error.message);
      })
  }

  
}


