import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IProduct } from '../interfaces/product/product';
import { Observable } from 'rxjs';
import { getAPIUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class ProductService {

  private apiUrl: string = getAPIUrl();

  constructor(public http: HttpClient) { }

  getAllProducts(): Observable<IProduct[]>{
    return this.http.get<IProduct[]>(this.apiUrl + "product")
  }

  getProductById(id: number): Observable<IProduct> {
    return this.http.get<IProduct>(this.apiUrl + "product/" + id)
  }

}


