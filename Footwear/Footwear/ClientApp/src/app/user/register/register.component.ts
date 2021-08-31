import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  form: FormGroup;

  constructor(private fb: FormBuilder, public userService: UserService, private toastr: ToastrService,
    private router: Router) {

    this.form = fb.group({
      email: ['', [Validators.required, Validators.email, Validators.maxLength(100)], []],
      passwords: this.fb.group({
        password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(100)], []],
        confirmPassword: ['', [Validators.required], []]
      }, { validator: this.confirmPasswords }),
      firstName: ['', [Validators.required, Validators.maxLength(100)], []],
      lastName: ['', [Validators.required, Validators.maxLength(100)], []],
      phone: ['', [Validators.required, Validators.maxLength(20)], []],
      address: ['', [Validators.required, Validators.maxLength(100)], []]
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
