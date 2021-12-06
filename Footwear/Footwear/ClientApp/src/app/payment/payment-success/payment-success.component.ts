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

  constructor(private orderService: OrderService, private toastr: ToastrService, private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => { this.sessionId = params['session_id'] });

    this.orderService.validatePayment(this.sessionId).subscribe((response: any) => {
      if (response.paymentStatus == 'Succeeded') {
        console.log(response.paymentStatus);
        this.toastr.show("Yeeeeeah!");
      }
      else {
        this.toastr.error("Error!", "Payment declined!");
      }
    })
  }

}
