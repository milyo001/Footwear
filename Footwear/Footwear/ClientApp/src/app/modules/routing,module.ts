import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router'; // CLI imports router
import { AboutComponent } from '../about/about.component';
import { AuthGuard } from '../interceptors/auth.guard';
import { CartComponent } from '../cart/cart.component';
import { HomeComponent } from '../home/home.component';
import { ProductSelectComponent } from '../product/product-select/product-select.component';
import { ProductComponent } from '../product/product/product.component';
import { LoginComponent } from '../user/login/login.component';
import { RegisterComponent } from '../user/register/register.component';
import { UserProfileComponent } from '../user/user-profile/user-profile.component';
import { OrdersComponent } from '../orders/orders.component';
import { PlaceOrderComponent } from '../orders/place-order/place-order.component';
import { PaymentSuccessComponent } from '../payment/payment-success/payment-success.component';
import { PaymentCancelComponent } from '../payment/payment-cancel/payment-cancel.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'products', component: ProductComponent }, //All products
  { path: 'products/:id', component: ProductSelectComponent }, //Product Details
  { path: 'about', component: AboutComponent },
  { path: 'cart', component: CartComponent, canActivate: [AuthGuard] },
  { path: 'user/login', component: LoginComponent },
  { path: 'user/register', component: RegisterComponent },
  { path: 'user/userProfile', component: UserProfileComponent },
  { path: 'placeOrder', component: PlaceOrderComponent },
  { path: 'cart/order', component: OrdersComponent },
  { path: 'payment-success', component: PaymentSuccessComponent },
  { path: 'payment-cancel', component: PaymentCancelComponent }
]; // sets up routes constant where you define your routes

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' })],
    
  exports: [RouterModule]
})
export class AppRoutingModule { }
