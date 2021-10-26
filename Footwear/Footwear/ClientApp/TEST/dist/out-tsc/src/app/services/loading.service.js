import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
let LoadingService = class LoadingService {
    constructor() {
        this._loading = new BehaviorSubject(false);
        this.loading = this._loading.asObservable();
    }
    show() {
        this._loading.next(true);
    }
    hide() {
        this._loading.next(false);
    }
};
LoadingService = __decorate([
    Injectable({
        providedIn: 'root',
    })
], LoadingService);
export { LoadingService };
//# sourceMappingURL=loading.service.js.map