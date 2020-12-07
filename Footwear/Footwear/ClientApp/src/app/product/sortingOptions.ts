import { IProduct } from "../interfaces";
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})

export class SortingOptions {

  constructor() { };

  sortProductsAscending(products: IProduct[]): IProduct[] {

    var sortedProducts: IProduct[] = products.sort(function (a, b) {

      var nameA = a.name.toUpperCase();
      var nameB = b.name.toUpperCase();

      if (nameA < nameB) {
        return -1;
      }
      if (nameA > nameB) {
        return 1;
      }
      return 0;
    });
    return sortedProducts;
  }

  sortProductsDescending(products: IProduct[]): IProduct[] {

    var sortedProducts: IProduct[] = products.sort(function (a, b) {

      var nameA = a.name.toUpperCase();
      var nameB = b.name.toUpperCase();

      if (nameA > nameB) {
        return -1;
      }
      if (nameA < nameB) {
        return 1;
      }
      return 0;
    });

    return sortedProducts;
  }

  sortProductsByPriceAscending(products: IProduct[]): IProduct[] {
    var sortedProducts: IProduct[] = products.sort((a, b) => a.price - b.price);
    return sortedProducts;
  }

  sortProductsByPriceDescending(products: IProduct[]): IProduct[] {
    var sortedProducts: IProduct[] = products.sort((a, b) => b.price - a.price);
    return sortedProducts;
  }

  //Sorts the data by default without doing a request to the web API again
  sortProductsByDefault(products: IProduct[]): IProduct[] {
    var sortedProducts: IProduct[] = products.sort((a, b) => a.id - b.id);
    return sortedProducts;
  }
}
