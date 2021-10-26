import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
let SortingOptions = class SortingOptions {
    constructor() { }
    ;
    sortProductsAscending(products) {
        var sortedProducts = products.sort(function (a, b) {
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
    sortProductsDescending(products) {
        var sortedProducts = products.sort(function (a, b) {
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
    sortProductsByPriceAscending(products) {
        var sortedProducts = products.sort((a, b) => a.price - b.price);
        return sortedProducts;
    }
    sortProductsByPriceDescending(products) {
        var sortedProducts = products.sort((a, b) => b.price - a.price);
        return sortedProducts;
    }
    //Sorts the data by default without doing a request to the web API again
    sortProductsByDefault(products) {
        var sortedProducts = products.sort((a, b) => a.id - b.id);
        return sortedProducts;
    }
};
SortingOptions = __decorate([
    Injectable({
        providedIn: 'root',
    })
], SortingOptions);
export { SortingOptions };
//# sourceMappingURL=sortingOptions.js.map