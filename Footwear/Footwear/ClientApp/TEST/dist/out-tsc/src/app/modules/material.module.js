import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
const modules = [
    MatToolbarModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule
];
let MaterialModule = class MaterialModule {
};
MaterialModule = __decorate([
    NgModule({
        declarations: [],
        imports: modules,
        exports: modules
    })
], MaterialModule);
export { MaterialModule };
//# sourceMappingURL=material.module.js.map