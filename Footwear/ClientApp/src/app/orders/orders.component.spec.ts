import { HttpClientTestingModule } from '@angular/common/http/testing';
import {
  ComponentFixture,
  discardPeriodicTasks,
  fakeAsync,
  flush,
  TestBed,
  tick,
} from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { of, throwError } from 'rxjs';
import { ICartProduct } from '../interfaces/cart/cartProduct';
import { ICompletedOrder } from '../interfaces/order/completedOrder';
import { IDeliveryInfo } from '../interfaces/order/deliveryInfo';
import { SharedModule } from '../modules/shared.module';
import { OrderService } from '../services/order.service';

import { OrdersComponent } from './orders.component';

const fakeProducts: ICartProduct[] = [
  {
    id: 1, details: 'tase', gender: 'sda', name: 'test', imageUrl: 'www.test.com', price: 20,
    quantity: 2, size: 44, productType: 'hiking', productId: 3,
  },
  {
    id: 2, details: 'tase', gender: 'dda', name: 'test', imageUrl: 'www.test.com',
    price: 20, quantity: 1, size: 33, productType: 'hiking', productId: 25,
  },
];

const fakeOrders: ICompletedOrder[] = [
  {
    userData: null,
    cartProducts: fakeProducts,
    createdOn: '12/2/2022',
    orderId: '213asd123z22xd',
    payment: 'card',
    status: 'completed',
  },
  {
    userData: null,
    cartProducts: fakeProducts,
    createdOn: '12/09/2021',
    orderId: '213asd123zxd',
    payment: 'cash',
    status: 'completed',
  },
];

const fakeDeliveryInfo: IDeliveryInfo = {
  deliveryPrice: 5,
  minDelivery: 1,
  maxDelivery: 5,
};

describe('OrdersComponent', () => {
  let component: OrdersComponent;
  let fixture: ComponentFixture<OrdersComponent>;
  let orderService: OrderService;
  let toastrService: ToastrService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OrdersComponent],
      imports: [
        BrowserAnimationsModule,
        ToastrModule.forRoot(),
        SharedModule,
        HttpClientTestingModule,
      ],
      providers: [OrderService, ToastrService],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrdersComponent);
    orderService = fixture.debugElement.injector.get(OrderService);
    toastrService = fixture.debugElement.injector.get(ToastrService);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeDefined();
  });

  it('should create #calculateDeliveryDate', () => {
    expect(component.calculateDeliveryDate).toBeDefined();
  });

  it('should create #cardPayment', () => {
    expect(component.cardPayment).toBeDefined();
  });

  it('should create #cashPayment', () => {
    expect(component.cashPayment).toBeDefined();
  });

  it('should create #closeDetailsSection', () => {
    expect(component.closeDetailsSection).toBeDefined();
  });

  it('should create #currentOrders', () => {
    component.currentOrders = fakeOrders;
    expect(component.currentOrders).toBeDefined();
    expect(component.currentOrders).toEqual(fakeOrders);
  });

  it('should create #pastOrders', () => {
    component.pastOrders = fakeOrders;
    expect(component.pastOrders).toBeDefined();
    expect(component.pastOrders).toEqual(fakeOrders);
  });

  it('should create #deliveryInfo', () => {
    const testInfo: IDeliveryInfo = {
      deliveryPrice: 22,
      maxDelivery: 2,
      minDelivery: 1,
    };
    component.deliveryInfo = testInfo;
    expect(component.deliveryInfo).toBeDefined();
    expect(component.deliveryInfo).toEqual(testInfo);
  });

  it('should create #detailsToggle', () => {
    expect(component.detailsToggle).toBeDefined();
  });

  it('should create icons', () => {
    expect(component.faBox).toBeDefined();
    expect(component.faCalendarDay).toBeDefined();
    expect(component.faCreditCard).toBeDefined();
    expect(component.faMoneyBill).toBeDefined();
  });

  it('#ngOnInit should detroy all orders array into current orders array', fakeAsync(() => {
    spyOn(orderService, 'getDeliveryPricingData').and.returnValue(
      of(fakeDeliveryInfo)
    );
    spyOn(orderService, 'getAllOrders').and.returnValue(of(fakeOrders));
    component.ngOnInit();
    tick(300);
    expect(component.currentOrders[0]).toEqual(fakeOrders[0]);
    flush();
  }));

  it('#ngOnInit should detroy all orders array into past orders array', fakeAsync(() => {
    spyOn(orderService, 'getDeliveryPricingData').and.returnValue(
      of(fakeDeliveryInfo)
    );
    spyOn(orderService, 'getAllOrders').and.returnValue(of(fakeOrders));
    component.ngOnInit();
    tick(300);
    expect(component.pastOrders[0]).toEqual(fakeOrders[1]);
    flush();
  }));

  it('#viewOrder should work as expected', () => {
    const fakeSelectedOrder = fakeOrders[0];
    fakeSelectedOrder.cartProducts = fakeProducts;

    component.selectedOrder = fakeSelectedOrder;
    component.deliveryInfo = fakeDeliveryInfo;
    component.viewOrderDetails();
    expect(component.totalOrderPrice).toEqual(65);
  });

  it('#sendEmail should work as expected', fakeAsync(() => {
    const emailBtnEl = { disabled: true };
    component.selectedOrder = fakeOrders[0];

    spyOn(orderService, 'sendEmailForOrder').and.returnValue(
      of({ sent: true })
    );
    spyOn(toastrService, 'info').and.callThrough();

    component.sendEmail(emailBtnEl);
    expect(toastrService.info).toHaveBeenCalledTimes(1);
    tick(5000);
    discardPeriodicTasks();
    expect(emailBtnEl.disabled).toEqual(false);
    flush();
  }));

  it('#sendEmail show error message when email not sent', fakeAsync(() => {
    const emailBtnEl = { disabled: true };
    component.selectedOrder = fakeOrders[0];
    const error: any = { error: { message: 'testError!' } };

    spyOn(orderService, 'sendEmailForOrder').and.returnValue(throwError(error));
    spyOn(toastrService, 'error').and.callThrough();

    component.sendEmail(emailBtnEl);
    tick(500);
    expect(emailBtnEl.disabled).toEqual(false);
    flush();
  }));

  it('#calculateDeliveryDate works as expected', () => {
    const date: Date = new Date('2024-01-01');
    const days = 3;
    const expectedDate = new Date('2024-01-04');
    const result = component.calculateDeliveryDate(date, days);
    expect(result).toEqual(expectedDate);
  });

  it('#onOrderChange works as expected', () => {
    const fakeEvent = { option: { value: fakeOrders[0]}};
    component.onOrderChange(fakeEvent);
    expect(component.selectedOrder).toEqual(fakeOrders[0]);
    expect(component.detailsToggle).toEqual(false);
  });


  it('#closeDetailsSection closes section as expected', fakeAsync(() => {
    const value = false;
    spyOn(component.ordersEl.nativeElement, 'scrollIntoView').and.callThrough();

    component.closeDetailsSection(value);
    expect(component.ordersEl.nativeElement.scrollIntoView).toHaveBeenCalled();
    tick(500);
    expect(component.detailsToggle = value);
    flush();
  }));


});
