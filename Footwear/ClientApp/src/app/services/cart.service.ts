import { Injectable } from '@angular/core';
import { ICartProduct } from '../interfaces/cart/cartProduct';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { getAPIUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  defaultQuantity: number = 1;  
  private apiUrl: string = getAPIUrl();

  constructor(private http: HttpClient) {}

  addToCart(id: number, size: number): Observable<Object> {
    const body = { id, size };
    return this.http.post(this.apiUrl + 'product/addToCart', body);
  }
  getAllCartProducts(): Observable<ICartProduct[]> {
    return this.http.get<ICartProduct[]>(this.apiUrl + "cart/getCartItems");
  }

  increaseProductQuantity(cartProductId: number): Observable<Object> {
    return this.http.put(this.apiUrl + 'cart/increaseProductQuantity', cartProductId);
  }

  decreaseProductQuantity(cartProductId: number): Observable<Object> {
    return this.http.put(this.apiUrl + 'cart/decreaseProductQuantity', cartProductId);
  }
  
  deleteCartProduct(cartProductId): Observable<ICartProduct> {
    return this.http.post<ICartProduct>(this.apiUrl + "cart/deleteCartProduct", cartProductId);
  }

  // Change the cart product isOrdered property to true, keep the cart products in the database
  removeAllCartProduts() {
    return this.http.delete(this.apiUrl + 'cart/removeCartProducts');
  }

}
