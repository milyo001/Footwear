import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IProduct } from '../interfaces/product';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private baseUrl: string;

  constructor(public http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getAllProducts(): Observable<IProduct[]>{
    return this.http.get<IProduct[]>(this.baseUrl + "product")
  }

  getProductById(id: number): Observable<IProduct> {
    return this.http.get<IProduct>(this.baseUrl + "product/" + id)
  }

}


