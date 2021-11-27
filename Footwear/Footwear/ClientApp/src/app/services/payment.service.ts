import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ICartProduct } from '../interfaces/cartProduct';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

    //Store products here passed from cart component to avoid additional request to the server
    products: ICartProduct[];
    baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  //Send the items to the server in the body
  checkout(items: ICartProduct[]) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json');
    return this.http.post(this.baseUrl + "create-checkout-session", items, { 'headers': headers });
  }


}
