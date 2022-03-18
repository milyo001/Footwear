import { Component, OnInit } from '@angular/core';
import { ICompletedOrder } from '../interfaces/order/completedOrder';
import { OrderService } from '../services/order.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  typesOfShoes: string[] = ['Boots', 'Clogs', 'Loafers', 'Moccasins', 'Sneakers'];
  name: string = "2198983298132981";
  productsCount: number = 5;
  status: string = "pending";

  currentOrders: ICompletedOrder[];
  completedOrders: ICompletedOrder[];

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
          [[], []]); // By default return empty array
    })
  };

  // Filters all order to current and past orders
  filerOrders(): void {

  }


}
