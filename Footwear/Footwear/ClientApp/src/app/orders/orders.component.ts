import { Component, OnInit } from '@angular/core';
import { ICompletedOrder } from '../interfaces/order/completedOrder';
import { OrderService } from '../services/order.service';
import { BoldPipe } from '../pipes/bold.pipe';

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

  orders: ICompletedOrder[];

  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
    this.orderService.getAllOrders().subscribe(orders => {
      this.orders = orders;
      console.log(orders);
    })
  };



}
