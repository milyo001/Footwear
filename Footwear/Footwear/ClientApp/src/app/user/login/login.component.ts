import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CookieService } from 'ngx-cookie-service';
import { LoadingService } from '../../services/loading.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loading = this.loader.loading;

  constructor(
    private userService: UserService,
    private router: Router,
    private toastr: ToastrService,
    private cookieService: CookieService,
    public loader: LoadingService) { }

  //The method will prevent any user from accessing the login view, who is already authenticated
  ngOnInit(): void {
    let test = this.cookieService.get('token');
    if (this.cookieService.get('token') != '') {
      this.router.navigate(['/']);
    }
  }

  //Send the token to the Web API to authenticate
  onSubmit(form: NgForm) {
    this.userService.login(form.value).subscribe(
      (response: any) => {
        this.cookieService.set('token', response.token)
        this.router.navigateByUrl('/');
      },
      error => {
        if (error.status == 400) { //bad request from the api
          this.toastr.error('Incorrect username and password.', 'Authentication failed.')
        } else {
          console.log(error);
        }
        
      }
    )
  }
}
