import { Component, OnInit } from '@angular/core';
import { ICompletedOrder } from '../interfaces/Order/completedOrder';

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

  constructor() { }

  ngOnInit(): void {

  }

}
