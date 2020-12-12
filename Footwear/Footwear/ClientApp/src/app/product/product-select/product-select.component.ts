import { Component, OnInit, Input } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IProduct } from '../../interfaces/product';
import { CartService } from '../../services/cart.service';
import { Local } from 'protractor/built/driverProviders';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product-select',
  templateUrl: './product-select.component.html',
  styleUrls: ['./product-select.component.css']
})
export class ProductSelectComponent {


  public selectedProduct: IProduct = null;

  public selectedSize: number;

  constructor(
    private productService: ProductService,
    private activatedRoute: ActivatedRoute,
    private cartService: CartService,
    private router: Router,
    private toastr: ToastrService
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
    if (localStorage.getItem('token')) {
      this.selectedProduct.size = this.selectedSize;
      this.cartService.addToCart(this.selectedProduct);
      this.toastr.success('Product successfully added to cart.','Product added.')
    } else {
      this.toastr.error('You need to be signed in to add to cart.', 'Please login.');
      this.router.navigate(['user/login']);
    }

    
  }

  sizeSelect(event: any) {
    this.selectedSize = event.target.value;
  }
}
