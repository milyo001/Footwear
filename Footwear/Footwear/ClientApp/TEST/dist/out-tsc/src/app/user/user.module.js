import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
let UserModule = class UserModule {
};
UserModule = __decorate([
    NgModule({
        declarations: [
            LoginComponent,
            RegisterComponent
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