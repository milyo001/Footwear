import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { finalize } from "rxjs/operators";
import { LoadingService } from "../services/loading.service";

@Injectable()
export class LoaderInterceptor implements HttpInterceptor {

  constructor(public loader: LoadingService) { }
  //This interceptor will show loader spinner with proccesing the all the http requests
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.loader.show();
    return next.handle(req).pipe(
      finalize(() => this.loader.hide())
    );
  }
}
