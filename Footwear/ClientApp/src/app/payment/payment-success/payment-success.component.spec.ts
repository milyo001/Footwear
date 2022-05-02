import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, fakeAsync, flush, TestBed, tick } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { SharedModule } from 'src/app/modules/shared.module';
import { CartService } from 'src/app/services/cart.service';
import { OrderService } from 'src/app/services/order.service';

import { PaymentSuccessComponent } from './payment-success.component';

describe('PaymentSuccessComponent', () => {
  let component: PaymentSuccessComponent;
  let fixture: ComponentFixture<PaymentSuccessComponent>;
  let orderService: OrderService;
  let toastrService: ToastrService;

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
        { provide: { ToastrService }, useValue: { toastrService }},
        { provide: ActivatedRoute, useValue: {
          queryParams: of({ session_id: 'klokkookok' })
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

  it('should show notification when paid with card', fakeAsync(() => {
    spyOn(orderService, 'validatePayment').and.returnValue(
    Observable.of( { paymentStatus:'paid' } ));
    toastrService = fixture.debugElement.injector.get(ToastrService);

    spyOn(toastrService, 'success').and.callThrough();
    component.ngOnInit();
    tick(300);
    // expect(toastrServiceSpy.success).toHaveBeenCalledTimes(1);
    expect(toastrService.success).toHaveBeenCalledTimes(1);
    flush();
  }));

  it('should show notification when paid with cash', fakeAsync(() => {
    spyOn(orderService, 'validatePayment').and.returnValue(
    Observable.of( { paymentStatus:'cash' } ));
    toastrService = fixture.debugElement.injector.get(ToastrService);

    spyOn(toastrService, 'success').and.callThrough();
    component.ngOnInit();
    tick(300);
    // expect(toastrServiceSpy.success).toHaveBeenCalledTimes(1);
    expect(toastrService.success).toHaveBeenCalledTimes(1);
    flush();
  }));


});
