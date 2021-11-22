import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ICartProduct } from '../interfaces/cartProduct';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

    products: ICartProduct[];
    baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  checkout(items) {
    var body = {};
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json');
    return this.http.post(this.baseUrl + "create-checkout-session", body, { 'headers': headers });
  }
}
