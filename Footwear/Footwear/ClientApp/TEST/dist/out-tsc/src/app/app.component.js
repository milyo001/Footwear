import { __decorate } from "tslib";
import { Component } from '@angular/core';
let AppComponent = class AppComponent {
    constructor(loader) {
        this.loader = loader;
        this.loading = this.loader.loading;
        this.title = 'Footwear';
    }
};
AppComponent = __decorate([
    Component({
        selector: 'app-root',
        templateUrl: './app.component.html',
        styleUrls: ['./app.component.css']
    })
], AppComponent);
export { AppComponent };
//# sourceMappingURL=app.component.js.map