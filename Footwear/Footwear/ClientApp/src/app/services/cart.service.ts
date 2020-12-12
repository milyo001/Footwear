import { Injectable } from '@angular/core';
import { IProduct } from '../interfaces';
import { ICartProduct } from '../interfaces/cartProduct';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  items: ICartProduct[] = [];
  itemStartIndex: number = 1;

  addToCart(product): void {
    const cartProduct: ICartProduct = {
      id: product.id,
      cartId: this.itemStartIndex, //used to remove the item
      name: product.name,
      size: product.size,
      details: product.details,
      imageUrl: product.imageUrl,
      gender: product.gender,
      productType: product.productType,
      price: product.price
    }

    this.itemStartIndex++;
    this.items.push(cartProduct);
  }

  getItems(): ICartProduct[] {
    return this.items;
  }

  clearCart(): ICartProduct[] {
    this.items = [];
    return this.items;
  }

  getTotalAmount() {
    const sum = this.items
      .map(item => item.price)
      .reduce((prev, curr) => prev + curr, 0);
    return sum;
  }

  deleteCartProduct(items: ICartProduct[], cartId: number): void {
    this.items = items.filter(product => product.cartId !== cartId);
  }
}
