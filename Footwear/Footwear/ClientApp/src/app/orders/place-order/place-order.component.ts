import { Component, OnInit } from '@angular/core';

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

  constructor() { }

  ngOnInit(): void {
  }


}


