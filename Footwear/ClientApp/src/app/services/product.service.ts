import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IProduct } from '../interfaces/product/product';
import { Observable } from 'rxjs';
import { getEnvAPIUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class ProductService {

  private apiUrl: string = getEnvAPIUrl();
 private headers = new HttpHeaders({
  'Content-Type': 'application/json',
  'Access-Control-Allow-Origin': '*' 
  });

  constructor(public http: HttpClient) { }

  getAllProducts(): Observable<IProduct[]>{
    return this.http.get<IProduct[]>(this.apiUrl + "product")
  }

  getProductById(id: number): Observable<IProduct> {
    return this.http.get<IProduct>(this.apiUrl + "product/" + id)
  }

}


