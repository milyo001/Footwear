import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { IProduct } from '../../interfaces/product/product';
import { SortingOptions } from '../sortingOptions';
import { LoadingService } from '../../services/loading.service';

@Component({
  selector: 'app-product-data',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  // Array used for sorting and filtering all the products
  public products: IProduct[] = [];

  // All products in their original state, fetched from the API
  untouchedProducts: IProduct[] = [];
  showContent: boolean = false;

  pageIndex: number = 1;

  constructor(
    private productService: ProductService,
    private sortingOptions: SortingOptions,
    public loader: LoadingService) { }

    ngOnInit(): void {
      this.productService.getAllProducts().subscribe(productsList => {
        this.products = productsList,
        this.untouchedProducts = productsList
      });
    };
  
  // Sorting options methods
  sortingAdvanced(event: any): void {
    const target: string = event.target.value;
    switch (target) {
      case "ascending":
        this.products = this.sortingOptions.sortProductsAscending(this.products);
        break;
      case "descending":
        this.products = this.sortingOptions.sortProductsDescending(this.products);
        break;
      case "ascendingPrice":
        this.products = this.sortingOptions.sortProductsByPriceAscending(this.products);
        break;
      case "descendingPrice":
        this.products = this.sortingOptions.sortProductsByPriceDescending(this.products)
        break;
      default:
        // Make a copy of the original array
        this.products = this.untouchedProducts.filter(() => true);
        break;
    }
    this.pageIndex = 1;
  }

  // Options values in HTML should be in PascalCase because the mapped Enums are PascalCase
  filterProducts(event: any): void {
    const dropdownValue = event.target.value;
    if (dropdownValue === 'Default') {
      this.products = [];
      // Make a copy of the original array
      this.products = this.untouchedProducts.filter(() => true);
    }
    else {
      const result = this.untouchedProducts.filter(product => product.gender.includes(dropdownValue));
      this.products = result;
    }
    this.pageIndex = 1;
  }

  // Once page is changed scroll to the top of the page
  scrollToTop(): void {
    document.querySelector('#all-products').scrollIntoView({
      behavior: 'smooth'
    });
  }
}


