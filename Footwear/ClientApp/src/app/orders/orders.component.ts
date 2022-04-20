import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ICompletedOrder } from '../interfaces/order/completedOrder';
import { OrderService } from '../services/order.service';
import {
  faCalendarDay, faBox,
  faCreditCard, faMoneyBill
} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  currentOrders: ICompletedOrder[];
  pastOrders: ICompletedOrder[];
  maxDeliveryDays: number = 0;
  pageIndex: number = 1;
  pageIndexPastOrders: number = 1;
  ordersPerPage: number = 10;

  // Order statuses recieved from API
  cashPayment: string = 'DeliveryCash';
  cardPayment: string = 'DeliveryPaid';

  // Icons
  faCalendarDay = faCalendarDay;
  faBox = faBox;
  faCreditCard = faCreditCard;
  faMoneyBill = faMoneyBill;

  constructor(private orderService: OrderService, private toastr: ToastrService) { }

  ngOnInit(): void {
    // Get max delivery days
    this.orderService.getDeliveryPricingData().subscribe(data => {
      this.maxDeliveryDays = data.maxDelivery;
    });

    this.orderService.getAllOrders().subscribe(orders => {
      // Use "deconstruction" style assignment
      [this.currentOrders, this.pastOrders] =
        orders
          .reduce((result, element) => {
            const today = new Date();
            const orderDate = new Date(element.createdOn);
            const maxDeliveryDate = this.calculateDeliveryDate(orderDate, this.maxDeliveryDays);

            // If the max delivery date is less than today's date push element to the first array
            result[maxDeliveryDate < today ? 1 : 0].push(element);
            return result;
          },
            // By default return empty array, can be further chained with map() or other functions.
            [[], []]);
    });
  };

  viewOrder(value: any) {
    console.log(this.currentOrders);
    console.log(this.pastOrders);

  }

  sendEmail(order, sendEmailBtn) {
    let id: string = order.orderId;
    sendEmailBtn.disabled = true;
    this.orderService.sendEmailForOrder(id).subscribe((response: any) => {
      if (response.sent) {
        this.toastr.info("Email sent!");
        // Disable send button for 5 sec to prevent spammy/unsolicited behavior
        setInterval(() => {
          sendEmailBtn.disabled = false;
        }, 5000);
      }
    },
      err => {
        this.toastr.info("Something went wrong", err.error.message);
        console.log(err);
        sendEmailBtn.disabled = false;
      })
  }

  // Returns the actual delivery date
  calculateDeliveryDate = (orderDate: Date, days: number) => {
    var date = new Date(orderDate);
    date.setDate(date.getDate() + days);
    return date;
  }
}
