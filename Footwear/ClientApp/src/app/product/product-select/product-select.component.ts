import { Component } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { IProduct } from '../../interfaces/product/product';
import { CartService } from '../../services/cart.service';
import { ToastrService } from 'ngx-toastr';
import { CookieService } from 'ngx-cookie-service';

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
    private toastr: ToastrService,
    private cookieService: CookieService,
    private _location: Location ) { }

  ngOnInit(): void {
    // When page is loaded scroll to the product view for better user experience
    document.getElementById("productFocus").scrollIntoView();

    let id: number = 0;
    //Get the product id from the URL parameters
    this.activatedRoute.params.subscribe(params => {
      id = params['id'];
    });

    this.productService.getProductById(id).subscribe((data: any) => {
      this.selectedProduct = data;

    }, err => {
      if (err.error.status == 404) {
        this.notFoundHandler();
      } else {
        this.toastr.error("Unknow error!", err.error.message);
        console.log(err.error);
      }
    });
  }

  // Add product to user's cart
  addToCart(): void {
    if (this.cookieService.get('token')) {
      const size: number = +((document.getElementById('size') as HTMLInputElement).value);
      const id: number = this.selectedProduct.id;

      this.cartService.addToCart(id, size).subscribe(
        (response: any) => {
          if (response.succeeded) {
            this.toastr.success('Product successfully added to cart.', 'Product added.')
          }
        },
        error => {
          this.toastr.error(error.error.message, 'Unable to add product!');
        }
      );
    } else {
      this.toastr.error('You need to be signed in to add to cart.', 'Please login.');
      this.router.navigate(['user/login']);
    }
  }

  goBack(): void {
    this._location.back();
  }

  // Redirect user to products when product is not found
  notFoundHandler(): void {
    this.toastr.error('Oops! Product not found!', 'Invalid product id.');
    this.router.navigate(['products']);
  }
}
