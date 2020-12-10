import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  isLoading = false;

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
  }

  loginHandler(formValue: { email: string, password: string }) {

    this.isLoading = true;

    //this.userService.login(formValue).subscribe({
    //  next: (data) => {
    //    this.isLoading = false;
    //    this.router.navigate(['/']);
    //  },
    //  error: (err) => {
    //    this.isLoading = false;
    //  }
    //});
  }
}
