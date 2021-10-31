import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { LoadingService } from '../../services/loading.service';


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
    public loader: LoadingService) {

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
    }
;

  onSubmit(formData) {
    
    this.userService.register(formData).subscribe(
      (response: any) => {
        if (response.succeeded) {
          this.form.reset();
          this.toastr.success("Registration successful.", 'Please log in.');
          this.router.navigate(['/user/login']);
        }
        else {
          console.log(response);
          response.errors.forEach(element => {
            switch (element.code) {
              case 'DuplicateUserName':
                this.toastr.error('Username already taken!', 'Registration failed.');
              break;  
              default:
                this.toastr.error(element.description, 'Registration failed.');
              break;
            }
          })
        }
      },
      err => {
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
