import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private userService: UserService, private router: Router, private toastr: ToastrService) { }

  //The method will prevent any user from accessing the login view, who is already authenticated
  ngOnInit(): void {
    if (localStorage.getItem('token') != null) {
      this.router.navigate(['/']);
    }
  }

  //Send the token to the Web API to authenticate
  onSubmit(form: NgForm) {
    this.userService.login(form.value).subscribe(
      (response: any) => {
        localStorage.setItem('token', response.token);
        localStorage.setItem('userName', form.value.email);
        console.log('UserName in local Storage: ' + form.value.email);
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
