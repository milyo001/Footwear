import { Component, Input, OnInit } from '@angular/core';
import { ICartProduct } from 'src/app/interfaces/cart/cartProduct';

@Component({
  selector: 'app-order-details-product',
  templateUrl: './order-details-product.component.html',
  styleUrls: ['./order-details-product.component.css']
})
export class OrderDetailsProductComponent implements OnInit {

  @Input() product: ICartProduct;

  constructor() { }

  ngOnInit(): void {
  }

  test(){
    console.log(this.product);
  }
  
}
