import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ProductComponent } from './product/product.component';
import { ProductSelectComponent } from './product-select/product-select.component';
import { AboutComponent } from './about/about.component';
import { CartComponent } from './cart/cart.component';
const routes = [
    { path: '', component: HomeComponent, pathMatch: 'full' },
    { path: 'products', component: ProductComponent },
    { path: 'products/:id', component: ProductSelectComponent },
    { path: 'about', component: AboutComponent },
    { path: 'cart', component: CartComponent },
];
let AppRoutingModule = class AppRoutingModule {
};
AppRoutingModule = __decorate([
    NgModule({
        declarations: [
            HomeComponent,
            ProductComponent,
            ProductSelectComponent,
            AboutComponent,
            CartComponent
        ],
        imports: [RouterModule.forRoot(routes)],
        exports: [RouterModule]
    })
], AppRoutingModule);
export { AppRoutingModule };
//# sourceMappingURL=app-routing.module.js.map