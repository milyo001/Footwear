import { Component, OnInit } from '@angular/core';
import { CartService } from '../services/cart.service';
import { IProduct } from '../interfaces';
import { faTrashAlt } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  cartProducts: IProduct[];
  totalAmount: number;

  //FontAwesome Icons:
  faTrashAlt = faTrashAlt;

  constructor(
    private cartService: CartService
  ) { }

  ngOnInit() {
    this.cartProducts = this.cartService.getItems();
    this.totalAmount = this.cartService.getTotalAmount();
  }

}
