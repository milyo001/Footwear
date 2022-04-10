import { Component, OnInit } from '@angular/core';
import { ICompletedOrder } from '../interfaces/order/completedOrder';
import { OrderService } from '../services/order.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  currentOrders: ICompletedOrder[];
  completedOrders: ICompletedOrder[];
  pageIndex: number = 1;
  ordersPerPage: number = 10;

  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
    this.orderService.getAllOrders().subscribe(orders => {
      // Use "deconstruction" style assignment
      [this.currentOrders, this.completedOrders] =
        orders
          .reduce((result, element) => {
            result[element.status == "Completed" ? 1 : 0].push(element); // Determine and push to current/completed orders array
            return result;
          },
            [[], []]); // By default return empty array, can be further chained with map() or other functions.
    });
  };
}
