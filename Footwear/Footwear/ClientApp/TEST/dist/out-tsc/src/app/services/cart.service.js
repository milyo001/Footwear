import { __decorate, __param } from "tslib";
import { Inject, Injectable } from '@angular/core';
let CartService = class CartService {
    constructor(http, baseUrl) {
        this.http = http;
        this.defaultQuantity = 1;
        this.baseUrl = baseUrl;
    }
    addToCart(product) {
        const body = {
            productId: product.id,
            name: product.name,
            size: product.size,
            details: product.details,
            imageUrl: product.imageUrl,
            gender: product.gender,
            productType: product.productType,
            price: product.price,
            quantity: this.defaultQuantity
        };
        return this.http.post(this.baseUrl + 'product/addToCart', body);
    }
    getAllCartProducts() {
        return this.http.get(this.baseUrl + "cart/getCartItems");
    }
    increaseProductQuantity(cartProductId) {
        return this.http.put(this.baseUrl + 'cart/increaseProductQuantity', cartProductId);
    }
    decreaseProductQuantity(cartProductId) {
        return this.http.put(this.baseUrl + 'cart/decreaseProductQuantity', cartProductId);
    }
    deleteCartProduct(cartProductId) {
        return this.http.post(this.baseUrl + "cart/deleteCartProduct", cartProductId);
    }
    clearCart() {
        return;
    }
    getTotalAmount() {
        //TODO
        //const sum = this.items
        //  .map(item => item.price)
        //  .reduce((prev, curr) => prev + curr, 0);
        return;
    }
    checkOut() {
    }
};
CartService = __decorate([
    Injectable({
        providedIn: 'root'
    }),
    __param(1, Inject('BASE_URL'))
], CartService);
export { CartService };
//# sourceMappingURL=cart.service.js.map