import { Component, OnInit, Input, AfterViewInit } from '@angular/core';
import { ICompletedOrder } from 'src/app/interfaces/order/completedOrder';
import { IDeliveryInfo } from 'src/app/interfaces/order/deliveryInfo';
import { faTruckLoading, faBarcode, faCalendarAlt,
  faWallet, faCompass, faMoneyCheckAlt
} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent implements AfterViewInit {

  @Input() order: ICompletedOrder;
  @Input() deliveryInfo: IDeliveryInfo;
  @Input() totalPrice: number;

  orderTotPrice: number = 0;

  // Icons
  faTruckLoading = faTruckLoading;
  faBarcode = faBarcode;
  faCalendarAlt = faCalendarAlt;
  faWallet = faWallet;
  faCompass = faCompass;
  faMoneyCheckAlt = faMoneyCheckAlt;

  constructor() {


    // const sum = this.order.cartProducts.reduce((accumulator, object) => {
    //   return accumulator + object.price;
    // }, 0);
    // this.orderTotPrice = sum;
  }
  ngAfterViewInit(): void {
    console.log(this.order?.cartProducts);
  }


  test(){
    console.log(this.deliveryInfo);
    console.log(this.order);
    console.log(this.orderTotPrice);
  }


}
