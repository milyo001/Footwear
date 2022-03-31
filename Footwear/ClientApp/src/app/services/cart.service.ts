import { Inject, Injectable } from '@angular/core';
import { ICartProduct } from '../interfaces/cart/cartProduct';
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

  addToCart(id: number, size: number): Observable<Object> {
    const body = { id, size };
    return this.http.post(this.baseUrl + 'product/addToCart', body);
  }
  getAllCartProducts(): Observable<ICartProduct[]> {
    return this.http.get<ICartProduct[]>(this.baseUrl + "cart/getCartItems");
  }

  increaseProductQuantity(cartProductId: number): Observable<Object> {
    return this.http.put(this.baseUrl + 'cart/increaseProductQuantity', cartProductId);
  }
  decreaseProductQuantity(cartProductId: number): Observable<Object> {
    return this.http.put(this.baseUrl + 'cart/decreaseProductQuantity', cartProductId);
  }
  
  deleteCartProduct(cartProductId): Observable<ICartProduct> {
    return this.http.post<ICartProduct>(this.baseUrl + "cart/deleteCartProduct", cartProductId);
  }

  //Change the cart product isOrdered property to true, keep the cart products in the database
  removeAllCartProduts() {
    return this.http.delete(this.baseUrl + 'cart/removeCartProducts');
  }

}
