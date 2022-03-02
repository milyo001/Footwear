import { Injectable, Injector } from '@angular/core';
import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { getBaseUrl } from '../../../environments/environment.test';
import { fakeEmailData, fakeUserData } from '../../user/user-profile/user-profile.component.spec'

@Injectable()
export class HttpRequestUserInterceptor implements HttpInterceptor {

  baseUrl: string = getBaseUrl();
  
  constructor(private injector: Injector) { }

  intercept(request: HttpRequest<any>, next: HttpHandler):
    Observable<HttpEvent<any>> {
    if (request.url) {
      if (request.url
        .indexOf(`${this.baseUrl}user/getProfileData`) > -1) {
        return of(new HttpResponse({
          status: 200, body: fakeUserData
        }));
      }
      if (request.url
        .indexOf(`${this.baseUrl}user/updateEmail`) > -1) {
        const headers = new HttpHeaders();
        headers.set("succeeded", "true");
        fakeUserData.email = fakeEmailData.email;
        return of(new HttpResponse({
          status: 200, body: fakeEmailData, headers
        }));
        
      }
    }
    
    

    return next.handle(request);
  }
}
