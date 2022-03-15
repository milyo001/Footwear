import { Component, OnInit } from '@angular/core';
import { ICompletedOrder } from '../interfaces/order/completedOrder';

import { LoadingService } from '../services/loading.service';
import { OrderService } from '../services/order.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  loading = this.loader.loading;
  orders: ICompletedOrder[] = [];

  constructor(public loader: LoadingService, private ordersService: OrderService) { }

  testFunct() {
    this.ordersService.getAllOrders().subscribe(orders => {
      this.orders = orders;
      console.log(this.orders);
    })
  }
}
