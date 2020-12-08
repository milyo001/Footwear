import { Component, OnInit, Input } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from '../../interfaces/product';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-product-select',
  templateUrl: './product-select.component.html',
  styleUrls: ['./product-select.component.css']
})
export class ProductSelectComponent {


  public selectedProduct: IProduct = null;

  constructor(
    private productService: ProductService,
    private activatedRoute: ActivatedRoute,
    private cartService: CartService
  ) { 

    let id: number = 0;

    //Get the product id from the URL parameters
    this.activatedRoute.params.subscribe(data => {
      id = data['id'];
    });

    productService.getProductById(id).subscribe(product => {
      this.selectedProduct = product;
    });
  }

  addToCart(product): void {
    this.cartService.addToCart(this.selectedProduct);
  }
}
