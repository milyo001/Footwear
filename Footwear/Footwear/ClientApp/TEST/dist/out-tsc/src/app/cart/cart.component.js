import { __decorate } from "tslib";
import { Component } from '@angular/core';
import { faInfoCircle, faTrashAlt } from '@fortawesome/free-solid-svg-icons';
import { ModalComponent } from './modal.component';
let CartComponent = class CartComponent {
    constructor(cartService, toastr, cookieService, router, modal, loader) {
        this.cartService = cartService;
        this.toastr = toastr;
        this.cookieService = cookieService;
        this.router = router;
        this.modal = modal;
        this.loader = loader;
        this.expandedIndex = 0;
        /*loading = this.loader.loading;*/
        //FontAwesome Icons:
        this.faTrashAlt = faTrashAlt;
        this.faInfoCircle = faInfoCircle;
    }
    //Load all the products from the dabase or display a notification (error message)
    ngOnInit() {
        if (this.cookieService.get('token') != '') {
            this.cartService.getAllCartProducts().subscribe(productsList => {
                this.cartProducts = productsList;
            });
        }
        else {
            this.toastr.error('You need to log in to view the cart!');
            this.router.navigate(['user/login']);
        }
    }
    ;
    //View product by given product id
    viewProduct(id) {
        this.router.navigate(['products/' + id]);
    }
    //This method will scroll the window to the accordion, when the accordion is closed
    scrollToAccordion(index, accordionItem) {
        if (accordionItem._expanded) {
            //Slow down the method so the accordion could close itself before scrolling the window
            setTimeout(() => {
                const element = document.getElementById("accordion-header-" + index);
                element.scrollIntoView({ behavior: 'smooth' });
            }, 50);
        }
    }
    //When clicked increase the quantity of a given item in cart.component.html and database
    //Increase the total price in the document
    incrementQuantity(cartProduct, index) {
        this.cartService.increaseProductQuantity(cartProduct.id).subscribe((response) => {
            if (response.succeeded) {
                this.increaseDomQuantity(index);
                this.increaseDomTotPrice(index, cartProduct.price);
            }
        }, err => {
            console.log(err);
        });
    }
    //When clicked decrease the quantity of a given item in the document and database
    //Decrease the total price in the document
    decrementQuantity(cartProduct, index) {
        var quantityElement = document.getElementById("quantity" + index);
        var value = parseInt(quantityElement.textContent);
        if (value <= 1) {
            this.toastr.warning("Cannot lower quantity.", "Quantity cannot be zero, try removing the item");
        }
        else {
            this.cartService.decreaseProductQuantity(cartProduct.id).subscribe((response) => {
                if (response.succeeded) {
                    if (value > 1) {
                        this.decreaseDomQuantity(quantityElement, value);
                        this.decreaseDomTotPrice(index, cartProduct.price);
                    }
                }
            }, err => {
                console.log(err);
            });
        }
    }
    //Ask for confirmation with modal and if user confirms removes item from database and the document
    deleteProduct(item, index) {
        const modalRef = this.modal.open(ModalComponent);
        modalRef.componentInstance.product = item;
        modalRef.result.then(result => {
            if (result == "confirm") {
                this.cartService.deleteCartProduct(item.id).subscribe((response) => {
                    if (response.succeeded) {
                        this.deleteCartProductEl(index);
                    }
                }, err => {
                    console.log(err);
                });
            }
        }, (reason) => { });
    }
    //DOM Manipulation Helpers
    increaseDomQuantity(index) {
        var quantityElement = document.getElementById("quantity" + index);
        var value = parseInt(quantityElement.textContent);
        quantityElement.textContent = (++value).toString();
    }
    increaseDomTotPrice(index, price) {
        var totalPrice = document.getElementById("totalPrice" + index);
        var totPriceElValue = parseFloat(totalPrice.textContent);
        totalPrice.textContent = (totPriceElValue + price).toFixed(2);
    }
    decreaseDomQuantity(quantityElement, value) {
        quantityElement.textContent = (--value).toString();
    }
    decreaseDomTotPrice(index, price) {
        var totalPrice = document.getElementById("totalPrice" + index);
        var totPriceElValue = parseFloat(totalPrice.textContent);
        totalPrice.textContent = (totPriceElValue - price).toFixed(2);
    }
    deleteCartProductEl(index) {
        const element = document.getElementById("accordion-header-" + index);
        element.remove();
    }
};
CartComponent = __decorate([
    Component({
        selector: 'app-cart',
        templateUrl: './cart.component.html',
        styleUrls: ['./cart.component.css']
    })
], CartComponent);
export { CartComponent };
//# sourceMappingURL=cart.component.js.map