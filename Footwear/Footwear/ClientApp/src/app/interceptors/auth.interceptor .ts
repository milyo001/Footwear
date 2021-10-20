import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { Router } from "@angular/router";
import { CookieService } from "ngx-cookie-service";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private router: Router, private cookieService: CookieService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
 
    if (this.cookieService.get('token') != null) {
      const clonedReq = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + this.cookieService.get('token'))
      });
      return next.handle(clonedReq).pipe(
        tap(
          succ => { },
          err => {
            if (err.status == 401) { //Send error if user is not logged in, check web api controller
              this.cookieService.delete('token')
              this.router.navigateByUrl('/user/login');
            }
          }
        )
      )
    }
    else
      return next.handle(req.clone());
  }
}
