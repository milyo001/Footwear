import { Injectable, Injector } from '@angular/core';
import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { getBaseUrl } from '../../../environments/environment.test';
import { fakeEmailData, fakeUpdateUserData, fakeUserData } from '../../user/user-profile/user-profile.component.spec'

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
        // After email is changed in the API, the client will reload the userData from the API,
        // calling the loadDataAsync()
        return of(new HttpResponse({
            status: 200, body: { succeeded: true }
        }));
      }
      if (request.url
        .indexOf(`${this.baseUrl}'user/updateUserProfile'`) > -1) {
        return of(new HttpResponse({
          status: 202, body: { succeeded: true }
        }));
      }

    }
    
    

    return next.handle(request);
  }
}
