import { __decorate } from "tslib";
import { Component } from '@angular/core';
import { SortingOptions } from './sortingOptions';
let ProductComponent = class ProductComponent {
    constructor(productService, sortingOptions) {
        this.productService = productService;
        this.sortingOptions = sortingOptions;
        //All products array used for data transfer from the web API
        this.products = [];
        this.sortingOptions = new SortingOptions();
    }
    ngOnInit() {
        this.productService.getAllProducts().subscribe(productsList => {
            this.products = productsList;
        });
        //The default number of pagination page is 1
        this.pageIndex = 1;
    }
    ;
    //Sorting methods:
    sortingChangeHandler(event) {
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
            this.products = this.sortingOptions.sortProductsByPriceDescending(this.products);
        }
        //Once items are sorted the user will be redirected to the default(first) page
        this.pageIndex = 1;
    }
};
ProductComponent = __decorate([
    Component({
        selector: 'app-product-data',
        templateUrl: './product.component.html',
        styleUrls: ['./product.component.css']
    })
], ProductComponent);
export { ProductComponent };
//# sourceMappingURL=product.component.js.map