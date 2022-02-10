import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IDeliveryInfo } from '../interfaces/Common/deliveryInfo';
import { IOrder } from '../interfaces/Order/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

   baseUrl: string;

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

  validatePayment(sessionId: string) {
    return this.http.get(this.baseUrl + 'order/payment-success/?session_id=' + sessionId);
  }

  getDeliveryPricingData(): Observable<IDeliveryInfo> {
    return this.http.get<IDeliveryInfo>(this.baseUrl + 'order/getDeliveryInfo');
  }
}
