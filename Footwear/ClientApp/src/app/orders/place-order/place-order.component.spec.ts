import { Location } from '@angular/common';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';
import { FormBuilder } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { ICartProduct } from 'src/app/interfaces/cart/cartProduct';
import { IDeliveryInfo } from 'src/app/interfaces/order/deliveryInfo';
import { SharedModule } from 'src/app/modules/shared.module';
import { CartService } from 'src/app/services/cart.service';
import { OrderService } from 'src/app/services/order.service';
import { UserService } from 'src/app/services/user.service';

import { PlaceOrderComponent } from './place-order.component';

const fakeDeliveryInfo: IDeliveryInfo = {
  deliveryPrice: 5,
  maxDelivery: 3,
  minDelivery: 1
};

const fakeProducts: ICartProduct[] = [
  { id: 1, details: 'tase', gender: 'sda', name: 'test', imageUrl: 'www.test.com', price: 50,
    quantity: 3, size: 44, productType: 'hiking', productId: 3,},
  { id: 2, details: 'tase', gender: 'dda', name: 'test', imageUrl: 'www.test.com',
    price: 50, quantity: 1, size: 33, productType: 'hiking', productId: 25 },
];

describe('PlaceOrderComponent', () => {
  let component: PlaceOrderComponent;
  let fixture: ComponentFixture<PlaceOrderComponent>;
  let orderService: OrderService;
  let toastrService: ToastrService;
  let userService: UserService;
  let cartService: CartService;
  let formBuilder: FormBuilder;
  let location: Location;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PlaceOrderComponent],
      imports: [
        BrowserAnimationsModule,
        ToastrModule.forRoot(),
        SharedModule,
        HttpClientTestingModule,
        RouterTestingModule,
      ],
      providers: [
        OrderService,
        ToastrService,
        UserService,
        CartService,
        FormBuilder,
        Location,
      ],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlaceOrderComponent);
    component = fixture.componentInstance;
    orderService = fixture.debugElement.injector.get(OrderService);
    toastrService = fixture.debugElement.injector.get(ToastrService);
    userService = fixture.debugElement.injector.get(UserService);
    cartService = fixture.debugElement.injector.get(CartService);
    formBuilder = fixture.debugElement.injector.get(FormBuilder);
    location = fixture.debugElement.injector.get(Location);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  // Component Properties

  it('should define #userData', () => {
    expect(component.userData).toBeDefined();
  });

  it('should define #deliveryInfo', () => {
    expect(component.deliveryInfo).toBeDefined();
  });

  it('should define #form', () => {
    expect(component.form).toBeDefined();
  });

  it('should define #totalPrice', () => {
    component.totalPrice = 199.99;
    expect(component.totalPrice).toBeDefined();
  });

  // Document properties

  it('should define #labelPosition', () => {
    expect(component.labelPosition).toBeDefined();
  });

  it('should define #paymentOptions', () => {
    expect(component.paymentOptions).toBeDefined();
  });

  it('should define #sort', () => {
    expect(component.sort).toBeDefined();
  });

  it('should define #displayedColumns', () => {
    expect(component.displayedColumns).toBeDefined();
  });

  it('should define #dataSource', () => {
    component.dataSource = new MatTableDataSource<ICartProduct>();
    expect(component.dataSource).toBeDefined();
  });

  it('should define #waitForRedirect', () => {
    expect(component.waitForRedirect).toBeDefined();
  });

  it('should define #waitForRedirect', () => {
    expect(component.waitForRedirect).toBeDefined();
  });

  it('#ngOnInit should work as expected', fakeAsync(() => {
    spyOn(orderService, 'getDeliveryPricingData')
      .and.returnValue(Observable.of(fakeDeliveryInfo));
    spyOn(cartService, 'getAllCartProducts')
      .and.returnValue(Observable.of(fakeProducts));
    component.ngOnInit();
    tick(300);
    expect(component.deliveryInfo).toEqual(fakeDeliveryInfo);
    expect(component.cartProducts).toEqual(fakeProducts);


  }));

});
