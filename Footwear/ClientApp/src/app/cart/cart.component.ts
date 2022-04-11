import { Component, OnInit } from '@angular/core';
import { CartService } from '../services/cart.service';
import { faInfoCircle, faTrashAlt } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from './modal.component';
import { LoadingService } from '../services/loading.service';
import { ICartProduct } from '../interfaces/cart/cartProduct';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})

export class CartComponent implements OnInit {

  cartProducts: ICartProduct[];

  //FontAwesome Icons:
  faTrashAlt = faTrashAlt;
  faInfoCircle = faInfoCircle;

  constructor(
    private cartService: CartService,
    private toastr: ToastrService,
    private router: Router,
    public modal: NgbModal,
    public loader: LoadingService
  ) { }

  // Load all the products from the dabase or display a notification (error message)
  ngOnInit() {
    this.cartService.getAllCartProducts().subscribe(productsList => {
      this.cartProducts = productsList;
    })
  };

  // View product by given product id
  viewProduct(id: number) {
    this.router.navigate(['products/' + id]);
  }

  // This method will scroll the window to the accordion, when the accordion is closed
  scrollToAccordion(index, accordionItem) {
    if (accordionItem._expanded) {
      // Slow down the method execution so the accordion could close itself before scrolling the window
      setTimeout(() => {
        const element = document.getElementById("accordion-header-" + index);
        element.scrollIntoView({ behavior: 'smooth' });
      }, 50);
    }
  }

  // Increase the quantity of item in the document and server
  incrementQuantity(cartProduct: ICartProduct): void {
    this.cartService.increaseProductQuantity(cartProduct.id).subscribe(
      (response: any) => {
        if (response.succeeded) {
          cartProduct.quantity++;
        }
      },
      err => {
        console.log(err);
        this.toastr.error("Cannot increase quantity!", "Internal server error!");
      }
    );
  }

  // Decrease the quantity of item in the document and server
  decrementQuantity(cartProduct: ICartProduct): void {
    if (cartProduct.quantity <= 1) {
      this.toastr.warning("Cannot lower quantity.", "Quantity cannot be zero, try to delete the item!");
    }
    else {
      this.cartService.decreaseProductQuantity(cartProduct.id).subscribe(
        (response: any) => {
          if (response.succeeded) {
            cartProduct.quantity--;
          }
        },
        err => {
          console.log(err);
          this.toastr.error("Cannot decrease quantity!", "Internal server error!");
        }
      );
    }
  }

  // Ask for confirmation with modal and if user confirms removes item from database and the document
  deleteProduct(item: ICartProduct, index: number) {
    const modalRef = this.modal.open(ModalComponent);
    modalRef.componentInstance.product = item;

    modalRef.result.then(result => {
      if (result === "confirm") {
        this.cartService.deleteCartProduct(item.id).subscribe((response: any) => {
          if (response.succeeded) {
            this.cartProducts.splice(index, 1);
          }
        },
          err => {
            console.log(err);
            this.toastr.error("Unable to remove item.", "Server error!");
          }
        );
      }
    }, (reason) => {
      console.log(reason);
      this.toastr.error("Unable to remove item.", reason);
    });
  }
}
