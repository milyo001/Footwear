import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICompletedOrder } from '../interfaces/order/completedOrder';
import { IDeliveryInfo } from '../interfaces/order/deliveryInfo';
import { IOrder } from '../interfaces/order/order';

@Injectable({
  providedIn: 'root'
})

export class OrderService {

   private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  //Redirect user to the stripe payment page 
  checkOut() {
    return this.http.get(this.baseUrl + "create-checkout-session");
  }

  //Send the order to the server in the body
  createOrder(order: IOrder) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json');
    return this.http.post(this.baseUrl + "order/create-order", order, { 'headers': headers });
  }

  // Send request to the API to validate if the payment was successfull
  validatePayment(sessionId: string) {
    return this.http.get(this.baseUrl + 'order/payment-success/?session_id=' + sessionId);
  }

  // Get the delivery data, example: delivery price and delivery time
  getDeliveryPricingData(): Observable<IDeliveryInfo> {
    return this.http.get<IDeliveryInfo>(this.baseUrl + 'order/getDeliveryInfo');
  }

  // Get all orders and all products for the orders
  getAllOrders(): Observable<ICompletedOrder> {
    return this.http.get<ICompletedOrder>(this.baseUrl + 'order/getAllOrders');
  }
}
