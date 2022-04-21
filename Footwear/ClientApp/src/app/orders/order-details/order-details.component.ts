import { Component, OnInit, Input } from '@angular/core';
import { ICompletedOrder } from 'src/app/interfaces/order/completedOrder';
import { IDeliveryInfo } from 'src/app/interfaces/order/deliveryInfo';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent implements OnInit {

  @Input() order: ICompletedOrder;
  @Input() deliveryInfo: IDeliveryInfo;

  constructor() { }

  ngOnInit(): void {
  }

  test(){
    console.log(this.order.createdOn);
    console.log(this.deliveryInfo);
  }
  

}
