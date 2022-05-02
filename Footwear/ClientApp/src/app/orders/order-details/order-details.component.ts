import { Component, Input, AfterViewInit, Output, EventEmitter } from '@angular/core';
import { ICompletedOrder } from 'src/app/interfaces/order/completedOrder';
import { IDeliveryInfo } from 'src/app/interfaces/order/deliveryInfo';
import { faTruckLoading, faBarcode, faCalendarAlt,
  faWallet, faCompass, faMoneyCheckAlt, faUser, faPhoneSquare,
  faAddressCard, faHandHoldingUsd
} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent {

  @Input() order: ICompletedOrder;
  @Input() deliveryInfo: IDeliveryInfo;
  @Input() totalPrice: number;

  // An event always emiting false boolean value to close the entire details section
  @Output() detailsToggleEvent = new EventEmitter<boolean>();;

  orderTotPrice: number = 0;

  // Used in mat-accordion
  panelOpenState: boolean = false;

  // Icons
  faTruckLoading = faTruckLoading;
  faBarcode = faBarcode;
  faCalendarAlt = faCalendarAlt;
  faWallet = faWallet;
  faCompass = faCompass;
  faMoneyCheckAlt = faMoneyCheckAlt;
  faUser = faUser;
  faPhoneSquare = faPhoneSquare;
  faAddress = faAddressCard;
  faHandHoldingUsd = faHandHoldingUsd;

  constructor() { }

  // Close the details section in parent component
  closeDetailsSection() {
    this.detailsToggleEvent.emit(false);
  }

}
