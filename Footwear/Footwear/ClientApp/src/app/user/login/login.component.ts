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

  ngOnInit(): void {
  }

  onSubmit(form: NgForm) {
    this.userService.login(form.value).subscribe(
      (response: any) => {
        localStorage.setItem('token', response.token);
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
