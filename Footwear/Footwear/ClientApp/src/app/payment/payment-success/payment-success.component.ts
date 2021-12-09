import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from '../../services/order.service';

@Component({
  selector: 'app-payment-success',
  templateUrl: './payment-success.component.html',
  styleUrls: ['./payment-success.component.css']
})
export class PaymentSuccessComponent implements OnInit {

  sessionId: string;

  constructor(private orderService: OrderService, private toastr: ToastrService, private route: ActivatedRoute,
              private router: Router  ) { }

  ngOnInit(): void {
    //Check if session exists in the URL, if present return session id, if not return undefined
    this.route.queryParams.subscribe(params => { this.sessionId = params['session_id'] });

    //Extra validation for card payment
    if (this.sessionId) {
      this.orderService.validatePayment(this.sessionId).subscribe((response: any) => {
        if (response.paymentStatus == 'paid') {
          this.toastr.success("Thank you!", "Payment successful!");
        }
        else {
          this.router.navigate(['/', 'payment-cancel'])
          this.toastr.error("Error!", "Payment declined!");
        }
      });
    }
    else {
      //Handle cash payment 
    }
    
  }

}
