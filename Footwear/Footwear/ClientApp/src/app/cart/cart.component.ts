import { Component, OnInit } from '@angular/core';
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
  incrementQuantity(cartProductId: number, index: number): void {

    const token = this.cookieService.get('token');

    //Send the id of the cart product and the user auth token to change the quantity in the database
    this.cartService.increaseProductQuantity(cartProductId, token).subscribe(
        (response: any) => {
        if (response.succeeded) {
          var quantityElement = document.getElementById("quantity" + index);
          var value = parseInt(quantityElement.textContent);
          quantityElement.textContent = (++value).toString();
          }
        },
        err => {
          console.log(err);
        }
      );
    
  }
  
  viewProduct(id: number) {
    this.router.navigate(['products/' + id]);
  }
  
}
