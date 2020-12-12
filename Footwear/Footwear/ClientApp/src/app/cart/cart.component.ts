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

  products: IProduct[];

  //FontAwesome Icons:
  faTrashAlt = faTrashAlt;

  constructor(
    private cartService: CartService
  ) { }

  ngOnInit() {
    this.products = this.cartService.getItems();
  }

}
