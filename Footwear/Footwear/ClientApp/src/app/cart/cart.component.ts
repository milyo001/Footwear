import { Component, OnInit } from '@angular/core';
import { CartService } from '../services/cart.service';
import { faInfoCircle, faTrashAlt } from '@fortawesome/free-solid-svg-icons';
import { ICartProduct } from '../interfaces/cartProduct';
import { ToastrService } from 'ngx-toastr';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from './modal.component';
import { LoadingService } from '../services/loading.service';
import { PaymentService } from '../services/payment.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  cartProducts: ICartProduct[];
  totalAmount: number;
  expandedIndex = 0;
  hasProducts: boolean;
  
  //FontAwesome Icons:
  faTrashAlt = faTrashAlt;
  faInfoCircle = faInfoCircle;

  constructor(
    private cartService: CartService,
    private paymentService: PaymentService,
    private toastr: ToastrService,
    private cookieService: CookieService,
    private router: Router,
    public modal: NgbModal,
    public loader: LoadingService
  ) {}


  //Load all the products from the dabase or display a notification (error message)
  ngOnInit() {
    if (this.cookieService.get('token') != '') {
      this.cartService.getAllCartProducts().subscribe(productsList => {
        this.cartProducts = productsList;
        if (this.cartProducts.length > 0) {
          this.hasProducts = true;
        }
      })
    }
    else {
      this.toastr.error('You need to log in to view the cart!');
      this.router.navigate(['user/login']);
    }
  };

  //View product by given product id
  viewProduct(id: number) {
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
  incrementQuantity(cartProduct: ICartProduct, index: number): void {
    this.cartService.increaseProductQuantity(cartProduct.id).subscribe(
      (response: any) => {
        if (response.succeeded) {
          this.increaseDomQuantity(index);
          this.increaseDomTotPrice(index, cartProduct.price);
          this.cartProducts[index].quantity++;
          console.log(this.cartProducts);
        }
      },
      err => {
        console.log(err);
      }
    );
  }
  //When clicked decrease the quantity of a given item in the document and database
  //Decrease the total price in the document
  decrementQuantity(cartProduct: ICartProduct, index: number): void {
    var quantityElement = document.getElementById("quantity" + index);
    var value = parseInt(quantityElement.textContent);
    if (value <= 1) {
      this.toastr.warning("Cannot lower quantity.", "Quantity cannot be zero, try to delete the item");
    }
    else {
      this.cartService.decreaseProductQuantity(cartProduct.id).subscribe(
        (response: any) => {
          if (response.succeeded) {
            if (value > 1) {
              this.decreaseDomQuantity(quantityElement, value);
              this.decreaseDomTotPrice(index, cartProduct.price);
              this.cartProducts[index].quantity--;
              console.log(this.cartProducts);
            }
          }
        },
        err => {
          console.log(err);
        }
      );
    }
  }
  //Ask for confirmation with modal and if user confirms removes item from database and the document
  deleteProduct(item: ICartProduct, index: number) {
    const modalRef = this.modal.open(ModalComponent);
    modalRef.componentInstance.product = item;
    modalRef.result.then(result => {
      if (result == "confirm") {
        this.cartService.deleteCartProduct(item.id).subscribe(
          (response: any) => {
            if (response.succeeded) {
              this.deleteCartProductEl(index);
              this.cartProducts.splice(index, 1);
              if (this.cartProducts.length == 0) {
                this.hasProducts = false;
              }
            }
          },
          err => {
            console.log(err);
          }
        );
      }
    },
      (reason) => { /*Handle rejection or leave blank*/ });

  }

  //DOM Manipulation Helpers
  increaseDomQuantity(index: number) {
    var quantityElement = document.getElementById("quantity" + index);
    var value = parseInt(quantityElement.textContent);
    quantityElement.textContent = (++value).toString();
  }
  increaseDomTotPrice(index: number, price: number) {
    var totalPrice = document.getElementById("totalPrice" + index);
    var totPriceElValue = parseFloat(totalPrice.textContent);
    totalPrice.textContent = (totPriceElValue + price).toFixed(2);
  }
  decreaseDomQuantity(quantityElement: HTMLElement, value) {
    quantityElement.textContent = (--value).toString();
  }
  decreaseDomTotPrice(index: number, price: number) {
    var totalPrice = document.getElementById("totalPrice" + index);
    var totPriceElValue = parseFloat(totalPrice.textContent);
    totalPrice.textContent = (totPriceElValue - price).toFixed(2);
  }
  deleteCartProductEl(index: number) {
    const element = document.getElementById("accordion-header-" + index);
    element.remove();
  }
}
