import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
let AuthGuard = class AuthGuard {
    constructor(router, cookieService) {
        this.router = router;
        this.cookieService = cookieService;
    }
    canActivate(route, state) {
        if (this.cookieService.get("token") != null) {
            return true;
        }
        else {
            this.router.navigate(['user/login']);
            return false;
        }
    }
};
AuthGuard = __decorate([
    Injectable({
        providedIn: 'root'
    })
], AuthGuard);
export { AuthGuard };
//# sourceMappingURL=auth.guard.js.map