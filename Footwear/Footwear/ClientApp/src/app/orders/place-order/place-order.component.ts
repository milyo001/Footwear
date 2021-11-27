import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  faCreditCard,
  faMoneyBillWave
} from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { ICartProduct } from '../../interfaces/cartProduct';
import { IPaymentProduct } from '../../interfaces/paymentProducts';
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

  constructor(
    private paymentService: PaymentService,
    private toastr: ToastrService,
    private userService: UserService,
    private cartService: CartService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
      this.cartService.getAllCartProducts().subscribe(products => { this.cartProducts = products })
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
    this.paymentService.checkout(this.cartProducts).subscribe((response: any) => {
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
      this.userService.getUserProfile().subscribe(result => {
        this.form.patchValue({
          firstName: result.firstName,
          lastName: result.lastName,
          phone: result.phone,
          street: result.street,
          city: result.city,
          state: result.state,
          country: result.country,
          zipCode: result.zipCode
        })
      })
    } else {
      //You can add this.form.clear() if you want to clear the form when clicked
    }
  }

  submitData(form) {
    if (form.value.payment == "card") {
      console.log("user will pay with card!");
    }
    else {
      console.log("user will pay with cash!")
    }
  }
}




