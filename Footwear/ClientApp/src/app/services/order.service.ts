import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { getAPIUrl } from 'src/environments/environment';
import { ICompletedOrder } from '../interfaces/order/completedOrder';
import { IDeliveryInfo } from '../interfaces/order/deliveryInfo';
import { IOrder } from '../interfaces/order/order';

@Injectable({
  providedIn: 'root'
})

export class OrderService {

  private apiUrl: string = getAPIUrl();

  constructor(private http: HttpClient) { }

  //Redirect user to the stripe payment page 
  checkOut() {
    return this.http.get(this.apiUrl + "create-checkout-session");
  }

  //Send the order to the server in the body
  createOrder(order: IOrder) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json');
    return this.http.post(this.apiUrl + "order/create-order", order, { 'headers': headers });
  }

  // Send request to the API to validate if the payment was successfull
  validatePayment(sessionId: string) {
    return this.http.get(this.apiUrl + 'order/payment-success/?session_id=' + sessionId);
  }

  // Get the delivery data, example: delivery price and delivery time
  getDeliveryPricingData(): Observable<IDeliveryInfo> {
    return this.http.get<IDeliveryInfo>(this.apiUrl + 'order/getDeliveryInfo');
  }

  // Get all orders and all products for the orders
  getAllOrders(): Observable<ICompletedOrder[]> {
    return this.http.get<ICompletedOrder[]>(this.apiUrl + 'order/getAllOrders');
  }

  sendEmailForOrder(id: string) {
    return this.http.post(this.apiUrl + 'email/send/' + id,
    { headers: { 'Content-Type': 'application/json' }});
  }
}
