import { __decorate } from "tslib";
import { Component } from '@angular/core';
import { faMapMarkedAlt, faEnvelope, faPhoneSquare } from '@fortawesome/free-solid-svg-icons';
let AboutComponent = class AboutComponent {
    constructor() {
        //FontAwesomeIcons
        this.faMapMarkedAlt = faMapMarkedAlt;
        this.faEnvelope = faEnvelope;
        this.faPhoneSquare = faPhoneSquare;
    }
    ngOnInit() {
    }
};
AboutComponent = __decorate([
    Component({
        selector: 'app-about',
        templateUrl: './about.component.html',
        styleUrls: ['./about.component.css']
    })
], AboutComponent);
export { AboutComponent };
//# sourceMappingURL=about.component.js.map