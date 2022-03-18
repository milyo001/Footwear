import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "../interceptors/auth.guard";
import { OrdersComponent } from "../orders/orders.component";
import { PlaceOrderComponent } from "../orders/place-order/place-order.component";
import { BoldPipe } from "../pipes/bold.pipe";
import { OrderStatusPipe } from "../pipes/order-status.pipe";
import { SharedModule } from "./shared.module";


const routes: Routes = [
  { path: '', component: OrdersComponent, canActivate: [AuthGuard] },
  { path: 'placeOrder', component: PlaceOrderComponent, canActivate: [AuthGuard] },
];

@NgModule({
  declarations: [
    PlaceOrderComponent,
    OrdersComponent,
    BoldPipe,
    OrderStatusPipe
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes),
  ],
  providers: [
    BoldPipe,
    OrderStatusPipe
  ],
  exports: [
    RouterModule
  ]
})
export class OrderModule { }
