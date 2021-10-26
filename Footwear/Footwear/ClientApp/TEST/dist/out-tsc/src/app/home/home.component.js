import { __decorate } from "tslib";
import { Component } from '@angular/core';
let HomeComponent = class HomeComponent {
    constructor(loader, http) {
        this.loader = loader;
        this.http = http;
        this.loading = this.loader.loading;
    }
    test() {
        this.http
            .get('https://reqres.in/api/products/3')
            .subscribe((res) => {
            console.log(res);
        });
    }
};
HomeComponent = __decorate([
    Component({
        selector: 'app-home',
        templateUrl: './home.component.html',
        styleUrls: ['./home.component.css']
    })
], HomeComponent);
export { HomeComponent };
//# sourceMappingURL=home.component.js.map