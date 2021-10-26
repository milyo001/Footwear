import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
let AuthGuard = class AuthGuard {
    constructor(router) {
        this.router = router;
    }
    canActivate(route, state) {
        if (localStorage.getItem('token') != null) {
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
//# sourceMappingURL=auth - Copy.guard.js.map