import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { Router } from "@angular/router";
import { CookieService } from "ngx-cookie-service";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private router: Router, private cookieService: CookieService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
 
    
    if (this.cookieService.get('token')) {
      const clonedRequest = req.clone({
         headers: req.headers.append('Authorization', `${this.cookieService.get('token')}`) 
        });
      return next.handle(clonedRequest)
    }
    else
      return next.handle(req.clone());
  }
}
