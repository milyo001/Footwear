import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router'; 
import { AboutComponent } from '../about/about.component';
import { AuthGuard } from '../interceptors/auth.guard';
import { CartComponent } from '../cart/cart.component';
import { PaymentSuccessComponent } from '../payment/payment-success/payment-success.component';
import { PaymentCancelComponent } from '../payment/payment-cancel/payment-cancel.component';

const routes: Routes = [
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: 'products', loadChildren: () => import('../modules/product.module').then(m => m.ProductModule) },
  { path: 'about', component: AboutComponent }, 
  { path: 'cart', component: CartComponent, canActivate: [AuthGuard] },
  { path: 'user', loadChildren: () => import('../modules/user.module').then(m => m.UserModule) },
  { path: 'orders', loadChildren: () => import('../modules/order.module').then(m => m.OrderModule) },
  { path: 'payment-success', component: PaymentSuccessComponent, canActivate: [AuthGuard] },
  { path: 'payment-cancel', component: PaymentCancelComponent, canActivate: [AuthGuard] }
]; 

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' })],
    
  exports: [RouterModule]
})
export class AppRoutingModule { }
