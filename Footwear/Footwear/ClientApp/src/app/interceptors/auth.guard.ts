import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router, private cookieService: CookieService, private toastr: ToastrService ) { }

  canActivate(): boolean {
    if (this.cookieService.check("token")) {
      return true;
    }
      this.router.navigate(['user/login']);
    this.toastr.warning("Please log in.", "Cannot access this page!");
      return false;
  }

}
