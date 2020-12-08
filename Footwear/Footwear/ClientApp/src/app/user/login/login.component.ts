import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  errorMessage = '';

  constructor() { }

  ngOnInit(): void {
  }

  loginHandler(formValue: { email: string, password: string }) {
    console.log(formValue);
  }
}
