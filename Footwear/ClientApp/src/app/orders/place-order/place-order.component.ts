import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import {
  faArrowCircleLeft,
  faArrowCircleRight,
  faCreditCard,
  faDollarSign,
  faEdit,
  faHandHoldingUsd,
  faMoneyBillWave,
  faShippingFast
}
  from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { ICartProduct } from '../../interfaces/Cart/cartProduct';
import { IDeliveryInfo } from '../../interfaces/order/deliveryInfo';
import { IOrder } from '../../interfaces/Order/order';
import { IUserData } from '../../interfaces/User/userData';
import { CartService } from '../../services/cart.service';
import { OrderService } from '../../services/order.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-place-order',
  templateUrl: './place-order.component.html',
  styleUrls: ['./place-order.component.css'],
  providers: [{
    // Used for overriding mat stepper default icons
    provide: STEPPER_GLOBAL_OPTIONS, useValue: { displayDefaultIndicatorType: false }
  }]
})
export class PlaceOrderComponent implements OnInit {

  // Component Properties
  userData: IUserData = null;
  deliveryInfo: IDeliveryInfo = null;
  form: FormGroup;
  private phoneRegex: string = '[- +()0-9]+';
  totalPrice: number;
 
  // Document properties
  labelPosition: 'import' | 'notImport' = 'notImport';
  paymentOptions: 'card' | 'cash' = 'cash';
  @ViewChild(MatSort) sort: MatSort;
  displayedColumns: string[] = ['name', 'size', 'price', 'quantity', 'totalPerItem'];
  waitForRedirect: boolean = false;
  dataSource: MatTableDataSource<ICartProduct>;

  // Font awesome icons
  faMoneyBillWave = faMoneyBillWave;
  faCreditCard = faCreditCard;
  faShippingFast = faShippingFast;
  faDollarSign = faDollarSign;
  faHandFoldingUsd = faHandHoldingUsd;
  faArrowCircleRight = faArrowCircleRight;
  faArrowCircleLeft = faArrowCircleLeft;
  faEdit = faEdit;

  // HTTP operations properties
  cartProducts: ICartProduct[];
  order: IOrder;

  constructor(
    private orderService: OrderService,
    private toastr: ToastrService,
    private userService: UserService,
    private cartService: CartService,
    private fb: FormBuilder,
    private router: Router
  ) { }

 // Will populate data into component properties from the database using the services
  ngOnInit(): void {
    this.orderService.getDeliveryPricingData().subscribe(info => {

      this.deliveryInfo = info;

      this.cartService.getAllCartProducts().subscribe(products => {
        this.cartProducts = products;
        this.initDataSort(products);
        this.GetTotalPrice(products);
      });
    });
    this.initForm();
  }

  // Init data source and apply sorting directive to it, used for table sorting
  initDataSort(products: ICartProduct[]): void {
    this.dataSource = new MatTableDataSource<ICartProduct>(products);
    this.dataSource.sort = this.sort;
  }

  // Init the form and set validators
  initForm(): void  {
    this.form = this.fb.group({
      firstName: ["", [Validators.required, Validators.maxLength(100)], []],
      lastName: ["", [Validators.required, Validators.maxLength(100)], []],
      phone: ["", [Validators.required, Validators.maxLength(20), Validators.pattern(this.phoneRegex)], []],
      street: ["", [Validators.required, Validators.maxLength(100), Validators.minLength(2)], []],
      state: ["", [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
      country: ["", [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
      city: ["", [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
      zipCode: ["", [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
      payment: ["", [Validators.required], []]
    });
  }

  // Sum products total price for all products and the delivery price and store it in the component property
  GetTotalPrice(products: ICartProduct[]) {
    let price = products.reduce
      ((total: number, product: ICartProduct) => total + (product.price * product.quantity), 0);
    price += this.deliveryInfo.deliveryPrice;
    this.totalPrice = price;
    };


  // Finalize order and redirect to stripe API for card payment
  onCheckOut(): void{
    this.orderService.checkOut().subscribe((response: any) => {
      // Show success message and then redirect user to the pre-build payment page
      this.toastr.success("Redirecting, please wait!");

      // Wait few seconds then redirect
      setTimeout(() => { window.location.href = response.Url }, 1000);
    },
      error => {
        if (error.status == 400) { //bad request from the api
          this.toastr.error(error.error.message, 'An unexpected error occured!');
          console.log(error);
        }
      })
  }

  // Creates an order with diffrent payment options
  createOrder(): void {
    this.orderService.createOrder(this.order).subscribe((response: any) => {
      if (response.cardPayment) {
        this.onCheckOut();
      }
      else {
        this.router.navigate(['/', 'payment-success'])
      }
    },
      error => {
        if (error.status == 400) { //bad request from the api
          this.toastr.error(error.error.message, 'Error,unable to create order!');
          console.log(error);
        }
        else {
          this.toastr.error('Error, unable to create order!');
        }
      })
  }

  // The methid will handle form values if user decides to import user information from account/userData
  handleImports(event)  {
    if (event.value == 'import') {
      // Patch value will set the form fields without validating them
      this.userService.getUserProfile().then(response => {
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
      // Optional: You can add this.form.clear() if you want to clear the form when Do not import is clicked
    }
  }

  // Submit the data from the form sent form the html template
  submitData(form) {
    // Disable form submit button to prevent dupplicate orders when double clicking
    this.waitForRedirect = true;

    var today = new Date();
    this.order = {
      orderId: null,
      createdOn: today.toUTCString(),
      payment: "cash", //Paying with cash by default
      status: "pending",
      // User data is the information about the delivery address, which can be diffrent
      // from the user data in Account/UserData 
      userData: form.value
    }

    // Set the payment type and send the order to the API
    if (form.value.payment == "card") {
      this.order.payment = "card";
      this.createOrder();
    }
    else {
      // Create order with default payment type of "cash"
      this.createOrder();
    }
  }
}




