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

  //Send the order to the server in the body
  checkoutCard(order: IOrder) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json');
    return this.http.post(this.baseUrl + "create-checkout-session", order, { 'headers': headers });
  }

  checkoutCash(order: IOrder) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json');
    return this.http.post(this.baseUrl + "create-checkout-session", order, { 'headers': headers });
  }

  createOrder(order: IOrder) {

    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json');
    return this.http.post(this.baseUrl + "order/create-order", order, { 'headers': headers });
  }

}
