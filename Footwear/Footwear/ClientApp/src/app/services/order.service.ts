import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { IOrder } from '../interfaces/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

   baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  //Redirect user to the stripe payment page 
  checkOutCard(order: IOrder) {
    return this.http.post(this.baseUrl + "create-checkout-session", order);
  }

  //Send the order to the server in the body
  createOrder(order: IOrder) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json');
    return this.http.post(this.baseUrl + "order/create-order", order, { 'headers': headers });
  }

}
