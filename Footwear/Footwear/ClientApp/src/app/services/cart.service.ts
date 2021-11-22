import { Inject, Injectable } from '@angular/core';
import { ICartProduct } from '../interfaces/cartProduct';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
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

  checkout(items) {
    var body = {};
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json');
    return this.http.post(this.baseUrl + "create-checkout-session", body, { 'headers': headers } );
  }

  addToCart(product): Observable<Object> {
    const body: ICartProduct = {
      productId: product.id,
      name: product.name,
      size: product.size,
      details: product.details,
      imageUrl: product.imageUrl,
      gender: product.gender,
      productType: product.productType,
      price: product.price,
      quantity: this.defaultQuantity
    }
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


}
