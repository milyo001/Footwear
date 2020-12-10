import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  form: FormGroup;
    

  constructor(private fb: FormBuilder, public userService: UserService) {

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
  };

  ngOnInit(): void {
  }

  registerHandler() {

  }

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
