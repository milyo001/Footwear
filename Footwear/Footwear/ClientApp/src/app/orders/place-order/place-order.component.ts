import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  faCreditCard,
  faMoneyBillWave
} from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { ICartProduct } from '../../interfaces/cartProduct';
import { IUserData } from '../../interfaces/userData';
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
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.cartProducts = this.paymentService.products;
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

  onCheckOut() {
    this.paymentService.checkout(this.cartProducts).subscribe((response: any) => {
      this.toastr.success("Redirecting, please wait!");
      setTimeout(() => { window.location.href = response.Url }, 3000);
    },
      error => {
        if (error.status == 400) { //bad request from the api
          this.toastr.error(error.error.message, 'An unexpected error occured!');
          console.log(error);
        }
      })
  }
  onCheckOutTest() {
    console.log(this.cartProducts);
  }

  handleImports(event) {
    if (event.value == 'import') {

    } else {
      this.form.reset();
    }

  }


  submitData(form) {
    console.log(form.value);
  }




}




