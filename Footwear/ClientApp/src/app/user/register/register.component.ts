import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { IRegisterData } from '../../interfaces/user/registerData';
import { CookieService } from 'ngx-cookie-service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {

  private emailRegex: string = '[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,63}$';
  private phoneRegex: string = '[- +()0-9]+';
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    public userService: UserService,
    private toastr: ToastrService,
    private router: Router,
    private cookieService: CookieService) {

    this.form = fb.group({
      email: ['', [Validators.required, Validators.pattern(this.emailRegex), Validators.maxLength(30)], []],
      passwords: this.fb.group({
        password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(100)], []],
        confirmPassword: ['', [Validators.required], []]
      }, { validator: this.confirmPasswords }),
      firstName: ['', [Validators.required, Validators.maxLength(100)], []],
      lastName: ['', [Validators.required, Validators.maxLength(100)], []],
      phone: ['', [Validators.required, Validators.maxLength(20), Validators.pattern(this.phoneRegex)], []]
    });
  }

  ngOnInit(): void {
    this.form.reset();
    if (this.cookieService.get('token') != '') {
      this.router.navigate(['/']);
    }
  };

  onSubmit(formData) {
    const registerData: IRegisterData = {
      email: formData.email,
      password: formData.passwords.password,
      firstName: formData.firstName,
      lastName: formData.lastName,
      phone: formData.phone
    };
    this.userService.register(registerData).subscribe(
      (response: any) => {
        if (response.succeeded) {
          this.form.reset();
          this.toastr.success("Registration successful.", 'Please log in.');
          this.router.navigate(['/user/login']);
        }
      },
      err => {
        this.toastr.error(err.error.message, "Registration failed!");
        console.log(err);
      }
    );
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
