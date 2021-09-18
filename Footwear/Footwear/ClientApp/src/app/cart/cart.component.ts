import { Component, OnInit } from '@angular/core';
import { CartService } from '../services/cart.service';
import { faInfoCircle, faTrashAlt } from '@fortawesome/free-solid-svg-icons';
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
  public isDetailsCollapsed = false;

  //FontAwesome Icons:
  faTrashAlt = faTrashAlt;
  faInfoCircle = faInfoCircle;

  constructor(
    private cartService: CartService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {

    const cartId = localStorage.getItem('cartId');

    this.cartService.getAllCartProducts(cartId).subscribe(productsList => {
      this.cartProducts = productsList;
    })
  };

}
