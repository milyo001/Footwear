import { __decorate } from "tslib";
import { Component } from '@angular/core';
import { SortingOptions } from '../sortingOptions';
let ProductComponent = class ProductComponent {
    constructor(productService, sortingOptions, loader) {
        this.productService = productService;
        this.sortingOptions = sortingOptions;
        this.loader = loader;
        //Array used for sorting and filtering all the products
        this.products = [];
        //All products in their original state
        this.untouchedProducts = [];
        this.showContent = false;
        this.sortingOptions = new SortingOptions();
    }
    ngOnInit() {
        this.productService.getAllProducts().subscribe(productsList => {
            this.products = productsList,
                this.untouchedProducts = productsList;
        });
        //The default number of pagination page is 1
        this.pageIndex = 1;
    }
    ;
    //Sorting methods:
    sortingAdvanced(event) {
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
            this.products = this.sortingOptions.sortProductsByPriceDescending(this.products);
        }
        else {
            this.products = [];
            //Make a copy of the original array
            this.products = this.untouchedProducts.filter(() => true);
        }
        this.pageIndex = 1;
    }
    //Options values in HTML should be in PascalCase because the mapped Enums are PascalCase
    filterProducts(event) {
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