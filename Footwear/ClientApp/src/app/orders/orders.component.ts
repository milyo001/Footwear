import { Component, OnInit } from '@angular/core';
import { ICompletedOrder } from '../interfaces/order/completedOrder';
import { OrderService } from '../services/order.service';
import { faCalendarDay } from '@fortawesome/free-solid-svg-icons';

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

  // Icons
  faCalendarDay = faCalendarDay;

  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
    this.orderService.getAllOrders().subscribe(orders => {
      // Use "deconstruction" style assignment
      [this.currentOrders, this.completedOrders] =
        orders
          .reduce((result, element) => {
            // Determine and push to current/completed orders array
            result[element.status == "Completed" ? 1 : 0].push(element);
            return result;
          },
          // By default return empty array, can be further chained with map() or other functions.
            [[], []]); 
    });
  };
}
