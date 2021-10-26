import { __decorate } from "tslib";
import { Component } from '@angular/core';
import { Validators } from '@angular/forms';
let UserProfileComponent = class UserProfileComponent {
    constructor(userService, fb) {
        this.userService = userService;
        this.fb = fb;
        this.userData = null;
        this.phoneRegex = '[- +()0-9]+';
    }
    ngOnInit() {
        this.loadData();
    }
    loadData() {
        this.userService.getUserProfile().subscribe(data => {
            this.userData = data;
            this.form = this.fb.group({
                firstName: [data.firstName, [Validators.required, Validators.maxLength(100)], []],
                lastName: [data.lastName, [Validators.required, Validators.maxLength(100)], []],
                phone: [data.phone, [Validators.required, Validators.maxLength(20), Validators.pattern(this.phoneRegex)], []],
                street: [data.street, [Validators.required, Validators.maxLength(100)], []],
                state: [data.state, [Validators.required, Validators.maxLength(20)], []],
                country: [data.country, [Validators.required, Validators.maxLength(20)], []],
                city: [data.city, [Validators.required, Validators.maxLength(20)], []],
                zipCode: [data.zipCode, [Validators.required, Validators.maxLength(20)], []]
            });
        });
    }
    updateProfile(form) {
    }
};
UserProfileComponent = __decorate([
    Component({
        selector: 'app-user-profile',
        templateUrl: './user-profile.component.html',
        styleUrls: ['./user-profile.component.css']
    })
], UserProfileComponent);
export { UserProfileComponent };
//# sourceMappingURL=user-profile.component.js.map