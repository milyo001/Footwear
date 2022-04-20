import { Component, OnInit, Input } from '@angular/core';
import { ICompletedOrder } from 'src/app/interfaces/order/completedOrder';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent implements OnInit {

  @Input() order: ICompletedOrder;
  
  constructor() { }

  ngOnInit(): void {
  }

  test(){
    console.log(this.order);
  }
}
