import { Component, OnInit, Type } from '@angular/core';
import { CartService } from '../services/cart.service';
import { faInfoCircle, faTrashAlt } from '@fortawesome/free-solid-svg-icons';
import { ICartProduct } from '../interfaces/cartProduct';
import { ToastrService } from 'ngx-toastr';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  cartProducts: ICartProduct[];
  totalAmount: number;
  expandedIndex = 0;

  //FontAwesome Icons:
  faTrashAlt = faTrashAlt;
  faInfoCircle = faInfoCircle;

  constructor(
    private cartService: CartService,
    private toastr: ToastrService,
    private cookieService: CookieService,
    private router: Router
  ) { }

  ngOnInit() {
    if (this.cookieService.get('token') != '') {
      this.cartService.getAllCartProducts().subscribe(productsList => {
        this.cartProducts = productsList;
      })
    }
    else {
      this.toastr.error('You need to log in to view the cart!');
      this.router.navigate(['user/login']);
    }
  };

  viewProduct(id: number) {
    this.router.navigate(['products/' + id]);
  }

  incrementQuantity(cartProduct: ICartProduct, index: number): void {
    //Send the id of the cart product and the user auth token to change the quantity in the database
    this.cartService.increaseProductQuantity(cartProduct.id).subscribe(
        (response: any) => {
        if (response.succeeded) {
          this.increaseDomQuantity(index);
          this.increaseDomTotPrice(index, cartProduct.price);
          }
        },
        err => {
          console.log(err);
        }
      );
  }

  decrementQuantity(cartProduct: ICartProduct, index: number): void {
    //Send the id of the cart product and to change the quantity in the database, index is the current
    //cartProducts collection index
    var quantityElement = document.getElementById("quantity" + index);
    var value = parseInt(quantityElement.textContent);
    if (value <= 1) {
      this.toastr.warning("Cannot lower quantity.", "Quantity cannot be zero, try removing the item");
    }
    else {
      this.cartService.decreaseProductQuantity(cartProduct.id).subscribe(
        (response: any) => {
          if (response.succeeded) {
            if (value > 1) {
              this.decreaseDomQuantity(quantityElement, value);
              this.decreaseDomTotPrice(index, cartProduct.price);
            }
          }
        },
        err => {
          console.log(err);
        }
      );
    }
  }

  deleteProduct(item: ICartProduct, index: number) {
    if (confirm("Are you sure to delete " + item.name)) {
      console.log("Implement delete functionality here");
    }
  }

  //DOM Manipulation
  increaseDomQuantity(index: number) {
    var quantityElement = document.getElementById("quantity" + index);
    var value = parseInt(quantityElement.textContent);
    quantityElement.textContent = (++value).toString();
  }
  increaseDomTotPrice(index: number, price: number) {
    var totalPrice = document.getElementById("totalPrice" + index);
    var totPriceElValue = parseInt(totalPrice.textContent);
    totalPrice.textContent = (totPriceElValue + price).toString();
  }
  decreaseDomQuantity(quantityElement: HTMLElement, value) {
    quantityElement.textContent = (--value).toString();
  }
  decreaseDomTotPrice(index: number, price:number) {
    var totalPrice = document.getElementById("totalPrice" + index);
    var totPriceElValue = parseInt(totalPrice.textContent);
    totalPrice.textContent = (totPriceElValue - price).toString();
  }
}
