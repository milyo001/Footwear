import { __decorate } from "tslib";
import { Injectable } from "@angular/core";
import { finalize } from "rxjs/operators";
let LoaderInterceptor = class LoaderInterceptor {
    constructor(loader) {
        this.loader = loader;
    }
    //This interceptor will show loader spinner with proccesing the all the http requests
    intercept(req, next) {
        this.loader.show();
        return next.handle(req).pipe(finalize(() => this.loader.hide()));
    }
};
LoaderInterceptor = __decorate([
    Injectable()
], LoaderInterceptor);
export { LoaderInterceptor };
//# sourceMappingURL=loader.interceptor.js.map