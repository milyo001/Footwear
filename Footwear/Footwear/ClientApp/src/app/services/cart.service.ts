import { Inject, Injectable } from '@angular/core';
import { IProduct } from '../interfaces';
import { ICartProduct } from '../interfaces/cartProduct';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Local } from 'protractor/built/driverProviders';
import { CookieService } from 'ngx-cookie-service';

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

  increaseProductQuantity(cartProductId: number): Observable<Object> {
    return this.http.put(this.baseUrl + 'cart/increaseProductQuantity', cartProductId);
  }
  decreaseProductQuantity(cartProductId: number): Observable<Object> {
    return this.http.put(this.baseUrl + 'cart/decreaseProductQuantity', cartProductId);
  }
  getAllCartProducts(): Observable<ICartProduct[]> {
    return this.http.get<ICartProduct[]>(this.baseUrl + "cart/getCartItems");
  }
  deleteCartProduct(cartProductId): Observable<ICartProduct> {
    return this.http.post<ICartProduct>(this.baseUrl + "cart/deleteCartProduct", cartProductId);
  }

  clearCart(): ICartProduct[] {
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

}
