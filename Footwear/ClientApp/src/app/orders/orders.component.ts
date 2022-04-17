import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {Location} from '@angular/common'; 
import { ICompletedOrder } from '../interfaces/order/completedOrder';
import { OrderService } from '../services/order.service';
import { 
  faCalendarDay, faBox, 
  faCreditCard, faMoneyBill }
from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  currentOrders: ICompletedOrder[];
  completedOrders: ICompletedOrder[];
  orderId : string;
  pageIndex: number = 1;
  ordersPerPage: number = 10;


  // Order statuses recieved from API
  cashPayment: string = 'DeliveryCash';
  cardPayment: string = 'DeliveryPaid';


  // Icons
  faCalendarDay = faCalendarDay;
  faBox = faBox;
  // Icons are declared here to render the icons returned as RAW HTML from order-status pipe
  faCreditCard = faCreditCard;
  faMoneyBill = faMoneyBill;

  constructor(private orderService: OrderService, private location: Location,
    private route: ActivatedRoute) { }

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

  viewOrder(value: any){
    console.log(value);
    console.log(value.toString());
    
  }

  sendEmail(){
    let id: string;

    this.orderService.sendEmailForOrder(id).subscribe(test => {
      console.log(test);
    },
    err => {
      console.log(err);
    })
  }

  changeUrl(orderId): void {
    this.orderId = orderId;
    // Set diffrent query param every time order is selected
    this.location.replaceState("orders/id=" + orderId);
  }
}
