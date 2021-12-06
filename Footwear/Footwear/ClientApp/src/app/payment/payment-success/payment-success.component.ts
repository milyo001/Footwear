import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from '../../services/order.service';

@Component({
  selector: 'app-payment-success',
  templateUrl: './payment-success.component.html',
  styleUrls: ['./payment-success.component.css']
})
export class PaymentSuccessComponent implements OnInit {

  sessionId: string;

  constructor(private orderService: OrderService, private toastr: ToastrService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    //Check if session exists in the URL, if present return session id, if not return undefined
    this.route.queryParams.subscribe(params => { this.sessionId = params['session_id'] });

    //Validation for card payment 
    if (this.sessionId) {
      this.orderService.validatePayment(this.sessionId).subscribe((response: any) => {
        if (response.paymentStatus == 'Succeeded') {
          this.toastr.success("Thank you!", "Payment successful!");
        }
        else {
          this.toastr.error("Error!", "Payment declined!");
        }
      });
    }
    else {
      //Handle cash payment 
    }
    
  }

}
