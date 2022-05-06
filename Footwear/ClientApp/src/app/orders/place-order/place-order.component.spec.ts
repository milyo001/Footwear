import { Location } from '@angular/common';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { SharedModule } from 'src/app/modules/shared.module';
import { CartService } from 'src/app/services/cart.service';
import { OrderService } from 'src/app/services/order.service';
import { UserService } from 'src/app/services/user.service';

import { PlaceOrderComponent } from './place-order.component';

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
});
