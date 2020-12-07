import { Injectable } from '@angular/core';
import { IProduct } from '../interfaces';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  items: IProduct[] = [];

  addToCart(product): void {
    this.items.push(product);
  }

  getItems(): IProduct[] {
    return this.items;
  }

  clearCart(): IProduct[] {
    this.items = [];
    return this.items;
  }

}
