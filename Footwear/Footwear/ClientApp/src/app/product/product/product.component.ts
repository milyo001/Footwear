import { Component } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { IProduct } from '../../interfaces/product';
import { SortingOptions } from '../sortingOptions';
import { LoadingService } from '../../services/loading.service';

@Component({
  selector: 'app-product-data',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent {

  //Array used for sorting and filtering all the products
  public products: IProduct[] = [];

  //All products in their original state
  public untouchedProducts: IProduct[] = [];

  //A declaration for the index for the ngx pagination package(used to render the first page of the list
  //when one of the sorting/filter methods are applied)
  public pageIndex: number;

  loading = this.loader.loading;

  constructor(private productService: ProductService, private sortingOptions: SortingOptions, public loader: LoadingService) {
    this.sortingOptions = new SortingOptions();
  }

  ngOnInit() {
    this.productService.getAllProducts().subscribe(productsList => {
      this.products = productsList,
      this.untouchedProducts = productsList
    });

    //The default number of pagination page is 1
    this.pageIndex = 1;
  };

  //Sorting methods:
  sortingAdvanced(event: any): void {

    const target = event.target.value;

    if (target == "ascending") {
      this.products = this.sortingOptions.sortProductsAscending(this.products);
      console.log(this.products);
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
    else {
      this.products = [];
      //Make a copy of the original array
      this.products = this.untouchedProducts.filter(() => true);
    }
    this.pageIndex = 1;
  }

  //Options values in HTML should be in PascalCase because the mapped Enums are PascalCase
  filterProducts(event: any): void {
    const dropdownValue = event.target.value;

    if (dropdownValue === 'Default') {
      this.products = [];
      //Make a copy of the original array
      this.products = this.untouchedProducts.filter(() => true);
    }
    else {
      const result = this.untouchedProducts.filter(product => product.gender.includes(dropdownValue));
      this.products = result;
    }
    this.pageIndex = 1;
  }

 
}


