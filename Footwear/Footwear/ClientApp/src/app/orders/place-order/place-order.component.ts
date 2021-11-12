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

  checked = false;
  indeterminate = false;
  labelPosition: 'import' | 'notImport' = 'notImport';
  disabled = false;

  //Font awesome icons
  faMoneyBillWave = faMoneyBillWave;
  faCreditCard = faCreditCard;

  constructor() { }

  ngOnInit(): void {
  }


}


