import { Component, OnInit, Input } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IProduct } from '../../interfaces/product';
import { CartService } from '../../services/cart.service';
import { ToastrService } from 'ngx-toastr';

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
      let size: number = +((document.getElementById('size') as HTMLInputElement).value);
      this.selectedProduct.size = size;
      this.cartService.addToCart(this.selectedProduct);
      this.addItemToLocal(this.selectedProduct);
      this.toastr.success('Product successfully added to cart.', 'Product added.')
    } else {
      this.toastr.error('You need to be signed in to add to cart.', 'Please login.');
      this.router.navigate(['user/login']);
    }
  }

  //This method will add the selected product into the local storage, this will prevent data loss when
  //document(web page) is refreshed
  private addItemToLocal(product): void {
    let localProducts = localStorage.getItem('products');
 
    if (localProducts == null) {
      let products: IProduct[] = [];
      products.push(product);
      localStorage.setItem('products', JSON.stringify(products));
    } else {
      let products: IProduct[] = JSON.parse(localStorage.getItem('products'));
      products.push(product);
      localStorage.setItem('products', JSON.stringify(products));
    }
  }
}
