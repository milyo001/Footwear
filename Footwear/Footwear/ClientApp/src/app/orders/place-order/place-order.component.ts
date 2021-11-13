import { Component, OnInit } from '@angular/core';
import {
  faCreditCard,
  faMoneyBillWave
} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-place-order',
  templateUrl: './place-order.component.html',
  styleUrls: ['./place-order.component.css']
})
export class PlaceOrderComponent implements OnInit {

  labelPosition: 'import' | 'notImport' = 'notImport';
  paymentOptions: 'card' | 'cash' = 'cash';
  //Font awesome icons
  faMoneyBillWave = faMoneyBillWave;
  faCreditCard = faCreditCard;

  constructor() { }

  ngOnInit(): void {
  }


  
}


