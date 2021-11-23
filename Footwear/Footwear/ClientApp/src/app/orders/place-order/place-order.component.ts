import { Component, OnInit } from '@angular/core';
import {
  faCreditCard,
  faMoneyBillWave
} from '@fortawesome/free-solid-svg-icons';
import { Toast, ToastrService } from 'ngx-toastr';
import { ICartProduct } from '../../interfaces/cartProduct';
import { PaymentService } from '../../services/payment.service';

@Component({
  selector: 'app-place-order',
  templateUrl: './place-order.component.html',
  styleUrls: ['./place-order.component.css']
})
export class PlaceOrderComponent implements OnInit {

  //Document properties
  labelPosition: 'import' | 'notImport' = 'notImport';
  paymentOptions: 'card' | 'cash' = 'cash';
  //Font awesome icons
  faMoneyBillWave = faMoneyBillWave;
  faCreditCard = faCreditCard;

  cartProducts: ICartProduct[];

  constructor(private paymentService: PaymentService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.cartProducts = this.paymentService.products;
  }


  onCheckOut() {
    this.paymentService.checkout(this.cartProducts).subscribe((response: any) => {
      this.toastr.success("Redirecting please wait!");
      setTimeout(() => { }, 3000);
      window.location.href = response.Url
    });
  }
  
}


