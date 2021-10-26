import { __decorate } from "tslib";
import { Component } from '@angular/core';
let ProductSelectComponent = class ProductSelectComponent {
    constructor(productService, activatedRoute, cartService) {
        this.productService = productService;
        this.activatedRoute = activatedRoute;
        this.cartService = cartService;
        this.selectedProduct = null;
        let id = 0;
        //Get the product id from the URL parameters
        this.activatedRoute.params.subscribe(data => {
            id = data['id'];
        });
        productService.getProductById(id).subscribe(product => {
            this.selectedProduct = product;
        });
    }
    addToCart(product) {
        this.cartService.addToCart(this.selectedProduct);
    }
};
ProductSelectComponent = __decorate([
    Component({
        selector: 'app-product-select',
        templateUrl: './product-select.component.html',
        styleUrls: ['./product-select.component.css']
    })
], ProductSelectComponent);
export { ProductSelectComponent };
//# sourceMappingURL=product-select.component.js.map