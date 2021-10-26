import { __decorate, __param } from "tslib";
import { Injectable, Inject } from '@angular/core';
let ProductService = class ProductService {
    constructor(http, baseUrl) {
        this.http = http;
        this.baseUrl = baseUrl;
    }
    getAllProducts() {
        return this.http.get(this.baseUrl + "product");
    }
    getProductById(id) {
        return this.http.get(this.baseUrl + "product/" + id);
    }
};
ProductService = __decorate([
    Injectable({
        providedIn: 'root'
    }),
    __param(1, Inject('BASE_URL'))
], ProductService);
export { ProductService };
//# sourceMappingURL=product.service.js.map