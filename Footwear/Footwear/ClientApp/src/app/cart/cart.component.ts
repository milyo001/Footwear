import { Component, OnInit } from '@angular/core';
import { CartService } from '../services/cart.service';
import { faTrashAlt } from '@fortawesome/free-solid-svg-icons';
import { ICartProduct } from '../interfaces/cartProduct';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  cartProducts: ICartProduct[];
  totalAmount: number;

  //FontAwesome Icons:
  faTrashAlt = faTrashAlt;

  constructor(
    private cartService: CartService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.refreshData();
  }

  deleteCartProduct(cartId) {
    this.cartService.deleteCartProduct(this.cartProducts, cartId);
    this.totalAmount = this.getTotalAmount();

    //An item in the cart service is deleted so updating the information is mandatory
    this.refreshData(); 
  }

  getTotalAmount(): number {
    return this.cartService.getTotalAmount();
  }
  onCheckOut(): void {
    this.toastr.success('Successfully created an order', 'Await delivery!')
    this.cartProducts = [];
    this.totalAmount = 0;
    this.cartService.checkOut();

  }

  refreshData() {
    this.cartProducts = this.cartService.getItems();
    this.totalAmount = this.getTotalAmount();
  }
}
