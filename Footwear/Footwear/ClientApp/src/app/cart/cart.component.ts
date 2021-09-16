import { Component, OnInit } from '@angular/core';
import { CartService } from '../services/cart.service';
import { faTrashAlt } from '@fortawesome/free-solid-svg-icons';
import { ICartProduct } from '../interfaces/cartProduct';
import { ToastrService } from 'ngx-toastr';
import { Local } from 'protractor/built/driverProviders';

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
    this.cartService.getAllCartProducts().subscribe(productsList => {
      this.cartProducts = productsList;
    })
  };

}
