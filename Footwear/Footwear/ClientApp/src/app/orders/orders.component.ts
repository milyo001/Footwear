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

      this.currentOrders = orders.filter((order: ICompletedOrder) => order.status != "Completed");
      this.completedOrders = orders.filter((order: ICompletedOrder) => order.status == "Completed");
    })
  };

  // Filters all order to current and past orders
  filerOrders(): void {

  }


}
