import { Inject, Injectable } from '@angular/core';
import { IProduct } from '../interfaces';
import { ICartProduct } from '../interfaces/cartProduct';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private baseUrl: string;
  defaultQuantity: number = 1;


  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  addToCart(product): Observable<Object> {

    const body: ICartProduct = {
      id: product.id,
      name: product.name,
      size: product.size,
      details: product.details,
      imageUrl: product.imageUrl,
      gender: product.gender,
      productType: product.productType,
      price: product.price,
      quantity: this.defaultQuantity,
      userName: localStorage.getItem('userName')
    }

    return this.http.post(this.baseUrl + 'product/addToCart', body);
  }

  getItems(): ICartProduct[] {
    //TODO
    return;
  }

  clearCart(): ICartProduct[] {
    //TODO

    //this.items = [];
    //return this.items;
    return;
  }

  getTotalAmount() {

    //TODO

    //const sum = this.items
    //  .map(item => item.price)
    //  .reduce((prev, curr) => prev + curr, 0);
    return
    
  }

  checkOut(): void {
    
  }

  deleteCartProduct(items: ICartProduct[], cartId: number): void {
    //TODO
  }
}
