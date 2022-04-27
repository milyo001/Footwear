import { Component, Input } from '@angular/core';
import { ICartProduct } from 'src/app/interfaces/cart/cartProduct';

@Component({
  selector: 'app-order-details-product',
  templateUrl: './order-details-product.component.html',
  styleUrls: ['./order-details-product.component.css']
})
export class OrderDetailsProductComponent {

  @Input() product: ICartProduct;

  constructor() { }
}
