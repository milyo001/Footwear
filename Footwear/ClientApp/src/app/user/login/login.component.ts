import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CookieService } from 'ngx-cookie-service';
import { ILoginData } from '../../interfaces/user/loginData';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  isChecked = true;

  constructor(
    private userService: UserService,
    private router: Router,
    private toastr: ToastrService,
    private cookieService: CookieService) { }

  // The method will prevent any user from accessing the login view from the URL address,
  // who is already authenticated
  ngOnInit(): void {
    if (this.cookieService.get('token') != '') {
      this.router.navigate(['/']);
    }
  }

  // Send a request the API to authenticate, generate, encrypt and return token
  onSubmit(form: NgForm) {
    const loginData: ILoginData = form.value;
    this.userService.login(loginData).subscribe(
      (response: any) => {
        if(this.isChecked){
          this.cookieService.set('token', response.token, { expires: 30} );
        }
        else {
          this.cookieService.set('token', response.token, { expires: 1} );
        }
        // this.cookieService.set('token', response.token)
        this.router.navigateByUrl('/');
      },
      error => {
        this.toastr.error(error.error.message, 'Authentication failed!');
        console.log(error);
      }
    )
  }
}
