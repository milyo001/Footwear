import { Component } from '@angular/core';
import { CartService } from '../services/cart.service';
import { IProduct } from '../interfaces';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  
  constructor(private cartService: CartService) { }

  cartItems: IProduct[] = this.cartService.getItems();

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
