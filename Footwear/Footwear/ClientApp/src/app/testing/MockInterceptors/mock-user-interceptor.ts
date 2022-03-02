import { Injectable, Injector } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { getBaseUrl } from '../../../environments/environment.test';
import { fakeUserData } from '../../user/user-profile/user-profile.component.spec'

@Injectable()
export class HttpRequestUserInterceptor implements HttpInterceptor {

  baseUrl: string = getBaseUrl();
  
  constructor(private injector: Injector) { }

  intercept(request: HttpRequest<any>, next: HttpHandler):
    Observable<HttpEvent<any>> {
    if (request.url && request.url
      .indexOf(`${this.baseUrl}/user/getProfileData`) > -1) {
      return of(new HttpResponse({
        status: 200, body: fakeUserData
      }));
    }

    return next.handle(request);
  }
}
