import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { ICompletedOrder } from '../interfaces/order/completedOrder';
import { IDeliveryInfo } from '../interfaces/order/deliveryInfo';
import { SharedModule } from '../modules/shared.module';
import { OrderService } from '../services/order.service';

import { OrdersComponent } from './orders.component';

describe('OrdersComponent', () => {
  let component: OrdersComponent;
  let fixture: ComponentFixture<OrdersComponent>;
  let orderService: OrderService;
  const testOrders: ICompletedOrder[] = [
    { userData: null, cartProducts: [null], createdOn: "12/2/2313",
      orderId:'213asd123zxd', payment: 'cash', status: "completed" },
    { userData: null, cartProducts: [null], createdOn: "12/2/2313",
      orderId:'213asd123z22xd', payment: 'card', status: "completed" }
  ];

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
    component.currentOrders = testOrders;
    expect(component.currentOrders).toBeDefined();
    expect(component.currentOrders).toEqual(testOrders);
  });

  it('should create #pastOrders', () => {
    component.pastOrders = testOrders;
    expect(component.pastOrders).toBeDefined();
    expect(component.pastOrders).toEqual(testOrders);
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
});
