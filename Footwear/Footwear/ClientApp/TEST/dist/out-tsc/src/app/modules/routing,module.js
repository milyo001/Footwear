import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router'; // CLI imports router
import { AboutComponent } from '../about/about.component';
import { AuthGuard } from '../interceptors/auth.guard';
import { CartComponent } from '../cart/cart.component';
import { HomeComponent } from '../home/home.component';
import { ProductSelectComponent } from '../product/product-select/product-select.component';
import { ProductComponent } from '../product/product/product.component';
import { LoginComponent } from '../user/login/login.component';
import { RegisterComponent } from '../user/register/register.component';
import { UserProfileComponent } from '../user/user-profile/user-profile.component';
const routes = [
    { path: '', component: HomeComponent, pathMatch: 'full' },
    { path: 'products', component: ProductComponent },
    { path: 'products/:id', component: ProductSelectComponent },
    { path: 'about', component: AboutComponent },
    { path: 'cart', component: CartComponent, canActivate: [AuthGuard] },
    { path: 'user/login', component: LoginComponent },
    { path: 'user/register', component: RegisterComponent },
    { path: 'user/userProfile', component: UserProfileComponent }
]; // sets up routes constant where you define your routes
// configures NgModule imports and exports
let AppRoutingModule = class AppRoutingModule {
};
AppRoutingModule = __decorate([
    NgModule({
        imports: [
            RouterModule.forRoot(routes)
        ],
        exports: [RouterModule]
    })
], AppRoutingModule);
export { AppRoutingModule };
//# sourceMappingURL=routing,module.js.map