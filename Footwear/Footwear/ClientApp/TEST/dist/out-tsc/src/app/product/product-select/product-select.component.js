import { __decorate } from "tslib";
import { Component } from '@angular/core';
let ProductSelectComponent = class ProductSelectComponent {
    constructor(productService, activatedRoute, cartService, router, toastr, cookieService, _location, loader) {
        this.productService = productService;
        this.activatedRoute = activatedRoute;
        this.cartService = cartService;
        this.router = router;
        this.toastr = toastr;
        this.cookieService = cookieService;
        this._location = _location;
        this.loader = loader;
        this.selectedProduct = null;
        this.loading = this.loader.loading;
        let id = 0;
        //Get the product id from the URL parameters
        this.activatedRoute.params.subscribe(data => {
            id = data['id'];
        });
        productService.getProductById(id).subscribe(product => {
            this.selectedProduct = product;
        });
    }
    ngOnInit() {
        //When page is loaded scroll to the product view for better user experience
        document.getElementById("productFocus").scrollIntoView();
    }
    addToCart(product) {
        if (this.cookieService.get('token')) {
            let size = +(document.getElementById('size').value);
            this.selectedProduct.size = size;
            this.cartService.addToCart(this.selectedProduct).subscribe((response) => {
                if (response.succeeded) {
                    this.toastr.success('Product successfully added to cart.', 'Product added.');
                }
            }, err => {
                console.log(err);
            });
        }
        else {
            this.toastr.error('You need to be signed in to add to cart.', 'Please login.');
            this.router.navigate(['user/login']);
        }
    }
    goBack() {
        this._location.back();
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