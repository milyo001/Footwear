import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  form: FormGroup;

  constructor(private fb: FormBuilder) {
    this.form = fb.group({
      email: ['', [Validators.required, Validators.email, Validators.maxLength(100)], [] ],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(100)], []],
      firstName: ['', [Validators.required, Validators.maxLength(100)], []],
      lastName: ['', [Validators.required, Validators.maxLength(100)], []],
      phone: ['', [Validators.required, Validators.maxLength(20)], []],
      address: ['', [Validators.required, Validators.maxLength(100)], []]
    });
  }

  ngOnInit(): void {
  }

  registerHandler() {

  }
}
