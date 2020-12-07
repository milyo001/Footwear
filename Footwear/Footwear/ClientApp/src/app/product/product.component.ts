import { Component } from '@angular/core';
import { ProductService } from '../services/product.service';
import { IProduct } from '../interfaces/product';
import { SortingOptions } from './sortingOptions';

@Component({
  selector: 'app-product-data',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent {

  //All products array used for data transfer from the web API
  public products: IProduct[] = [];


  //A declaration for the index for the ngx pagination package(used to render the first page of the list
  //when one of the sorting methods are applied)
  public pageIndex: number; 

  constructor(private productService: ProductService, private sortingOptions: SortingOptions) {

    this.sortingOptions = new SortingOptions();
  }

  ngOnInit() {
    this.productService.getAllProducts().subscribe(productsList => {
      this.products = productsList
    });

    //The default number of pagination page is 1
    this.pageIndex = 1;
  };

  //Sorting methods:
  sortingChangeHandler(event: any): void {

    const target = event.target.value;

    if (target == "ascending") {
      this.products = this.sortingOptions.sortProductsAscending(this.products);
    }
    else if (target == "descending") {
      this.products = this.sortingOptions.sortProductsDescending(this.products);
    }
    else if (target == "ascendingPrice") {
      this.products = this.sortingOptions.sortProductsByPriceAscending(this.products);
    }
    else if (target == "descendingPrice") {
      this.products = this.sortingOptions.sortProductsByPriceDescending(this.products)
    }
    //Once items are sorted the user will be redirected to the default(first) page
    this.pageIndex = 1;
  }

 
}


