import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { SharedModule } from 'src/app/modules/shared.module';
import { CartService } from 'src/app/services/cart.service';
import { OrderService } from 'src/app/services/order.service';

import { PaymentSuccessComponent } from './payment-success.component';

describe('PaymentSuccessComponent', () => {
  let component: PaymentSuccessComponent;
  let fixture: ComponentFixture<PaymentSuccessComponent>;
  let orderService: OrderService;
  let activatedRoute: ActivatedRoute;


  beforeEach(async () => {

    await TestBed.configureTestingModule({
      declarations: [ PaymentSuccessComponent ],
      imports: [
        BrowserAnimationsModule,
        SharedModule,
        ToastrModule.forRoot(),
        HttpClientTestingModule,
        RouterTestingModule.withRoutes([])
       ],
      providers: [
        OrderService,
        CartService,
        ToastrService,
        { provide: ActivatedRoute, useValue: {
          queryParams: of([{ session_id: 1 }])
          }
        },
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PaymentSuccessComponent);
    component = fixture.componentInstance;
    orderService = fixture.debugElement.injector.get(OrderService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should implements ngOnInit', () => {
    expect(component.ngOnInit).toBeTruthy();
  });

});
