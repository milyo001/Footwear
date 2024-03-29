import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ICompletedOrder } from '../interfaces/order/completedOrder';
import { OrderService } from '../services/order.service';
import {
  faCalendarDay,
  faBox,
  faCreditCard,
  faMoneyBill,
} from '@fortawesome/free-solid-svg-icons';
import { IDeliveryInfo } from '../interfaces/order/deliveryInfo';
import { ICartProduct } from '../interfaces/cart/cartProduct';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
})
export class OrdersComponent implements OnInit {

  currentOrders: ICompletedOrder[];
  pastOrders: ICompletedOrder[];
  deliveryInfo: IDeliveryInfo;
  selectedOrder: ICompletedOrder;
  totalOrderPrice: number = 0;

  // Document properties
  detailsToggle: boolean = false;
  @ViewChild('details') detailsEl: ElementRef;
  @ViewChild('ordersContainer') ordersEl: ElementRef;

  // Pagination options
  pageIndex: number = 1;
  pageIndexPastOrders: number = 1;
  ordersPerPage: number = 8;

  // Order statuses recieved from API
  cashPayment: string = 'DeliveryCash';
  cardPayment: string = 'DeliveryPaid';

  // Icons
  faCalendarDay = faCalendarDay;
  faBox = faBox;
  faCreditCard = faCreditCard;
  faMoneyBill = faMoneyBill;

  constructor(
    private orderService: OrderService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    // Get max delivery days
    this.orderService.getDeliveryPricingData().subscribe((data) => {
      this.deliveryInfo = data;
    });

    this.orderService.getAllOrders().subscribe((orders) => {
      // Use "deconstruction" style assignment
      [this.currentOrders, this.pastOrders] = orders.reduce(
        (result, order) => {
          const today = new Date();
          const orderDate = new Date(order.createdOn);
          const maxDeliveryDate = this.calculateDeliveryDate(orderDate,this.deliveryInfo.maxDelivery);

          // If the max delivery date is less than today's date push element to the first array
          // (current orders)
          result[maxDeliveryDate < today ? 1 : 0].push(order);
          return result;
        },
        // By default return empty array, can be further chained with map() or other functions.
        [[], []]
      );
    });
  }

  // View the selected order full details
  viewOrderDetails() {
    const products: ICartProduct[] = this.selectedOrder.cartProducts;
    this.detailsToggle = true;
    this.totalOrderPrice = this.orderService
      .calculateTotalPrice(products, this.deliveryInfo.deliveryPrice);

    setTimeout(() => {
      this.detailsEl.nativeElement.scrollIntoView({ behavior: 'smooth' });
    }, 100);
  }

  sendEmail(sendEmailBtn: any) {
    const id = this.selectedOrder.orderId;
    sendEmailBtn.disabled = true;

    this.orderService.sendEmailForOrder(id).subscribe(
      (response: any) => {
        if (response.sent) {
          this.toastr.info('Email sent!');
          // Disable send button for 5 sec to prevent spammy/unsolicited behavior
          setInterval(() => {
            sendEmailBtn.disabled = false;
          }, 5000);
        }
      },
      (err): any => {
        this.toastr.error('Something went wrong', err.error.message);
        console.log(err);
        sendEmailBtn.disabled = false;
      }
    );
  }

  // Returns the actual delivery date by given max delivery days
  calculateDeliveryDate = (orderDate: Date, days: number) => {
    orderDate.setDate(orderDate.getDate() + days);
    return orderDate;
  };

  // Set the property selectedOrder when order is changed in mat-list-option list of elements
  onOrderChange(event: any) {
    this.selectedOrder = event.option.value;
    this.detailsToggle = false;
  }

  // Close the details section when the close button is clicked in child component
  closeDetailsSection(value: boolean){
    this.ordersEl.nativeElement.scrollIntoView({ behavior: 'smooth' });
    // Scroll to all orders element then close the details section with small delay
    // for better user experience
    setTimeout(()=> {
      this.detailsToggle = value;
    }, 400)
  }
}
