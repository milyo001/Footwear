import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { IDeliveryInfo } from 'src/app/interfaces/order/deliveryInfo';
import { SharedModule } from 'src/app/modules/shared.module';
import { OrderDetailsComponent } from './order-details.component';

describe('OrderDetailsComponent', () => {
  let component: OrderDetailsComponent;
  let fixture: ComponentFixture<OrderDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OrderDetailsComponent],
      imports: [BrowserAnimationsModule, SharedModule],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should define #closeDetailsSection', () => {
    expect(component.closeDetailsSection).toBeDefined();
  });

  it('should define @Input #deliveryInfo', () => {
    const deliveryInfo: IDeliveryInfo = {
      deliveryPrice: 5.5,
      maxDelivery: 3,
      minDelivery: 1,
    };

    component.deliveryInfo = deliveryInfo;

    expect(component.deliveryInfo).toBeDefined();
    expect(component.deliveryInfo).toEqual(deliveryInfo);
  });

  it('should define #detailsToggleEvent', () => {
    expect(component.detailsToggleEvent).toBeTruthy();
  });

  it('should define @Input #totalPrice', () => {
    const totalPrice: number = 225.55;
    component.totalPrice = totalPrice;

    expect(component.totalPrice).toBeTruthy();
    expect(component.totalPrice).toEqual(totalPrice);
  });

  it('should define icons', () => {
    expect(component.faAddress).toBeTruthy();
    expect(component.faBarcode).toBeTruthy();
    expect(component.faCalendarAlt).toBeTruthy();
    expect(component.faCompass).toBeTruthy();
    expect(component.faHandHoldingUsd).toBeTruthy();
    expect(component.faMoneyCheckAlt).toBeTruthy();
    expect(component.faPhoneSquare).toBeTruthy();
    expect(component.faTruckLoading).toBeTruthy();
    expect(component.faUser).toBeTruthy();
    expect(component.faWallet).toBeTruthy();
  });

  it('#panelOpenState should be false', () => {
    expect(component.panelOpenState).toBeFalse();
  });
});
