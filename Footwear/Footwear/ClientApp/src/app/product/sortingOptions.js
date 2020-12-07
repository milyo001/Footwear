"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SortingOptions = void 0;
var SortingOptions = /** @class */ (function () {
    function SortingOptions() {
    }
    ;
    SortingOptions.prototype.sortProductsAscending = function (products) {
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
    };
    SortingOptions.prototype.sortProductsDescending = function (products) {
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
    };
    SortingOptions.prototype.sortProductsByPriceAscending = function (products) {
        var sortedProducts = products.sort(function (a, b) { return a.price - b.price; });
        return sortedProducts;
    };
    SortingOptions.prototype.sortProductsByPriceDescending = function (products) {
        var sortedProducts = products.sort(function (a, b) { return b.price - a.price; });
        return sortedProducts;
    };
    //Sorts the data by default without doing a request to the web API again
    SortingOptions.prototype.sortProductsByDefault = function (products) {
        var sortedProducts = products.sort(function (a, b) { return a.id - b.id; });
        return sortedProducts;
    };
    return SortingOptions;
}());
exports.SortingOptions = SortingOptions;
//# sourceMappingURL=sortingOptions.js.map