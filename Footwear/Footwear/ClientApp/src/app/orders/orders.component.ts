import { Component, OnInit } from '@angular/core';
import { IOrder } from '../interfaces/order';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  typesOfShoes: string[] = ['Boots', 'Clogs', 'Loafers', 'Moccasins', 'Sneakers'];
  orders: {};

  constructor() { }

  ngOnInit(): void {

  }

}
