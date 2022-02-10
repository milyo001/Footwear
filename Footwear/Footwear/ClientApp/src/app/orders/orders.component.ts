import { Component, OnInit } from '@angular/core';
import { ICompletedOrder } from '../interfaces/Order/completedOrder';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  typesOfShoes: string[] = ['Boots', 'Clogs', 'Loafers', 'Moccasins', 'Sneakers'];
  orders: ICompletedOrder[];

  constructor() { }

  ngOnInit(): void {

  }

}
