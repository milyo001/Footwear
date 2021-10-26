import { __decorate } from "tslib";
import { Injectable } from "@angular/core";
import { tap } from "rxjs/operators";
let AuthInterceptor = class AuthInterceptor {
    constructor(router, cookieService, loader) {
        this.router = router;
        this.cookieService = cookieService;
        this.loader = loader;
    }
    intercept(req, next) {
        this.loader.show();
        if (this.cookieService.get('token') != null) {
            const clonedReq = req.clone({
                headers: req.headers.set('Authorization', 'Bearer ' + this.cookieService.get('token'))
            });
            return next.handle(clonedReq).pipe(tap(succ => {
                this.loader.hide();
            }, err => {
                if (err.status == 401) { //Send error if user is not logged in, check web api controller
                    this.cookieService.delete('token');
                    this.router.navigateByUrl('/user/login');
                }
            }));
        }
        else
            return next.handle(req.clone());
    }
};
AuthInterceptor = __decorate([
    Injectable()
], AuthInterceptor);
export { AuthInterceptor };
//# sourceMappingURL=auth.interceptor%20.js.map