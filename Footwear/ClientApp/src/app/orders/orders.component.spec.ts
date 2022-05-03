import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, fakeAsync, flush, TestBed, tick } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { ICompletedOrder } from '../interfaces/order/completedOrder';
import { IDeliveryInfo } from '../interfaces/order/deliveryInfo';
import { SharedModule } from '../modules/shared.module';
import { OrderService } from '../services/order.service';

import { OrdersComponent } from './orders.component';

const fakeOrders: ICompletedOrder[] = [
  { userData: null, cartProducts: [null], createdOn: "12/2/2022",
    orderId:'213asd123z22xd', payment: 'card', status: "completed" },
  { userData: null, cartProducts: [null], createdOn: "12/09/2021",
    orderId:'213asd123zxd', payment: 'cash', status: "completed" },
];

const fakeDeliveryInfo: IDeliveryInfo = {
  deliveryPrice: 2.99,
  minDelivery: 1,
  maxDelivery: 5
}
describe('OrdersComponent', () => {
  let component: OrdersComponent;
  let fixture: ComponentFixture<OrdersComponent>;
  let orderService: OrderService;


  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrdersComponent ],
      imports: [
        BrowserAnimationsModule,
        ToastrModule.forRoot(),
        SharedModule,
        HttpClientTestingModule
      ],
      providers: [

        OrderService,
        ToastrService ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrdersComponent);
    orderService = fixture.debugElement.injector.get(OrderService);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeDefined();
  });

  it('should create #calculateDeliveryDate', () => {
    expect(component.calculateDeliveryDate).toBeDefined();
  });

  it('should create #calculateTotalPrice', () => {
    expect(component.calculateTotalPrice).toBeDefined();
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
    const testInfo: IDeliveryInfo = { deliveryPrice: 22, maxDelivery: 2, minDelivery:1 }
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
    spyOn(orderService, 'getDeliveryPricingData').and.returnValue( of(fakeDeliveryInfo));
    spyOn(orderService, 'getAllOrders').and.returnValue( of(fakeOrders));
    component.ngOnInit();
    tick(300);
    expect(component.currentOrders[0]).toEqual(fakeOrders[0]);
    flush();
  }));

  it('#ngOnInit should detroy all orders array into past orders array', fakeAsync(() => {
    spyOn(orderService, 'getDeliveryPricingData').and.returnValue( of(fakeDeliveryInfo));
    spyOn(orderService, 'getAllOrders').and.returnValue( of(fakeOrders));
    component.ngOnInit();
    tick(300);
    expect(component.pastOrders[0]).toEqual(fakeOrders[1]);
    flush();
  }));
});
