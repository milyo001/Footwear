import { DataSource } from '@angular/cdk/collections';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import {
    faArrowCircleLeft,
    faArrowCircleRight,
  faCreditCard,
  faDollarSign,
  faHandHoldingUsd,
  faMoneyBillWave,
  faShippingFast
}
  from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { ICartProduct } from '../../interfaces/cartProduct';
import { IDeliveryInfo } from '../../interfaces/deliveryInfo';
import { IOrder } from '../../interfaces/order';
import { IUserData } from '../../interfaces/userData';
import { CartService } from '../../services/cart.service';
import { OrderService } from '../../services/order.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-place-order',
  templateUrl: './place-order.component.html',
  styleUrls: ['./place-order.component.css']
})
export class PlaceOrderComponent implements OnInit, AfterViewInit {

  //Component Properties
  public userData: IUserData = null;
  public deliveryInfo: IDeliveryInfo = null;
  form: FormGroup;
  private phoneRegex: string = '[- +()0-9]+';
  public totalPrice: number;

  //Document properties
  labelPosition: 'import' | 'notImport' = 'notImport';
  paymentOptions: 'card' | 'cash' = 'cash';
  @ViewChild(MatSort) sort: MatSort;
  displayedColumns: string[] = ['name', 'size', 'price', 'quantity', 'totalPerItem'];
  dataSource: MatTableDataSource<ICartProduct>;


  //Font awesome icons
  faMoneyBillWave = faMoneyBillWave;
  faCreditCard = faCreditCard;
  faShippingFast = faShippingFast;
  faDollarSign = faDollarSign;
  faHandFoldingUsd = faHandHoldingUsd;
  faArrowCircleRight = faArrowCircleRight;
  faArrowCircleLeft = faArrowCircleLeft;

  //HTTP operations properties
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

 
  ngOnInit(): void {
    this.orderService.getDeliveryPricingData().subscribe(info => {
      this.deliveryInfo = info;
      this.cartService.getAllCartProducts().subscribe(products => {
        this.cartProducts = products;
        this.dataSource = new MatTableDataSource(products);
        console.log("Datasort after init: ", this.dataSource);
        this.GetTotalPrice(products);
        //Gets the min and max delivery days and the cost of the delivery

      });
    });

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

  //Add sorting to the dataSource, used for table sorting
  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    console.log("Datasort after sort set: ", this.dataSource);
  }


  //Gets products total price for all products and delivery price and store it in the component property
  GetTotalPrice(products: ICartProduct[]) {
    let price = products.reduce
      ((total: number, product: ICartProduct) => total + (product.price * product.quantity), 0);
    price += this.deliveryInfo.deliveryPrice;
    this.totalPrice = price;
    };


  onCheckOut(): void{
    this.orderService.checkOut(this.order).subscribe((response: any) => {
      //Show success message and then redirect user to the pre-build payment page
      this.toastr.success("Redirecting, please wait!");
      //Wait few seconds then redirect
      setTimeout(() => { window.location.href = response.Url }, 1000);
    },
      error => {
        if (error.status == 400) { //bad request from the api
          this.toastr.error(error.error.message, 'An unexpected error occured!');
          console.log(error);
        }
      })
  }


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
      })
  }

  handleImports(event)  {
    if (event.value == 'import') {
      //Patch value will set the form fields without validating them
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
      //You can add this.form.clear() if you want to clear the form when clicked
    }
  }

  submitData(form) {
    const fvalue = form.value;
    var today = new Date();
    this.order = {
      createdOn: today.toUTCString(),
      payment: "cash", //Paying with cash by default
      status: "pending",
      //user data is the address that user can select and could be different from the account/userdata
      userData: {
        firstName: fvalue.firstName,
        lastName: fvalue.lastName,
        email: fvalue.email,
        phone: fvalue.phone,
        street: fvalue.street,
        city: fvalue.city,
        state: fvalue.state,
        country: fvalue.country,
        zipCode: fvalue.zipCode
      }
    }
    //Set the payment type and send the order to the API
    if (form.value.payment == "card") {
      this.order.payment = "card";
      this.createOrder();
    }
    else {
      this.createOrder();
    }
  }
}




