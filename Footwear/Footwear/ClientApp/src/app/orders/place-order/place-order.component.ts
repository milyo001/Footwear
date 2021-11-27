import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  faCreditCard,
  faMoneyBillWave
} from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { ICartProduct } from '../../interfaces/cartProduct';
import { IOrder } from '../../interfaces/order';
import { IUserData } from '../../interfaces/userData';
import { CartService } from '../../services/cart.service';
import { PaymentService } from '../../services/payment.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-place-order',
  templateUrl: './place-order.component.html',
  styleUrls: ['./place-order.component.css']
})
export class PlaceOrderComponent implements OnInit {

  public userData: IUserData = null;
  form: FormGroup;
  private phoneRegex: string = '[- +()0-9]+';

  //Document properties
  labelPosition: 'import' | 'notImport' = 'notImport';
  paymentOptions: 'card' | 'cash' = 'cash';
  //Font awesome icons
  faMoneyBillWave = faMoneyBillWave;
  faCreditCard = faCreditCard;

  cartProducts: ICartProduct[];
  order: IOrder;

  constructor(
    private paymentService: PaymentService,
    private toastr: ToastrService,
    private userService: UserService,
    private cartService: CartService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.cartService.getAllCartProducts().subscribe(products => { this.cartProducts = products });
      this.form = this.fb.group({
        firstName: ["", [Validators.required, Validators.maxLength(100)], []],
        lastName: ["", [Validators.required, Validators.maxLength(100)], []],
        phone: ["", [Validators.required, Validators.maxLength(20), Validators.pattern(this.phoneRegex)], []],
        street: ["", [Validators.required, Validators.maxLength(100), Validators.minLength(2)], []],
        state: ["", [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
        country: ["", [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
        city: ["", [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
        zipCode: ["", [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
        payment: ["", [Validators.required],[]]
      });
 };

  onCheckOut(): void{
    this.paymentService.checkoutCard(this.order).subscribe((response: any) => {
      //Show success message and then redirect user to the pre-build payment page
      this.toastr.success("Redirecting, please wait!");
      //Wait few seconds then redirect
      setTimeout(() => { window.location.href = response.Url }, 3000);
    },
      error => {
        if (error.status == 400) { //bad request from the api
          this.toastr.error(error.error.message, 'An unexpected error occured!');
          console.log(error);
        }
      })
  }

  handleImports(event): void {
    if (event.value == 'import') {
      //Patch value will set the form fields without validating them
      this.userService.getUserProfile().subscribe(response => {
        this.form.patchValue({
          firstName: response.firstName,
          lastName: response.lastName,
          phone: response.phone,
          street: response.street,
          city: response.city,
          state: response.state,
          country: response.country,
          zipCode: response.zipCode
        })
      })
    } else {
      //You can add this.form.clear() if you want to clear the form when clicked
    }
  }

  submitData(form) {
    if (form.value.payment == "card") {
      var today = new Date();
      this.order = {
        products: this.cartProducts,
        payment: "card",
        createdOn: today.toUTCString(),
        status: "pending",
        userData: form.value
      }
      console.log(this.order);
      
    }
    else {
      console.log("user will pay with cash!")
    }
  }
}



