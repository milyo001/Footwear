import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from '../user/login/login.component';
import { RegisterComponent } from '../user/register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserProfileComponent } from '../user/user-profile/user-profile.component';
let UserModule = class UserModule {
};
UserModule = __decorate([
    NgModule({
        declarations: [
            LoginComponent,
            RegisterComponent,
            UserProfileComponent
        ],
        imports: [
            CommonModule,
            FormsModule,
            ReactiveFormsModule
        ]
    })
], UserModule);
export { UserModule };
//# sourceMappingURL=user.module.js.map