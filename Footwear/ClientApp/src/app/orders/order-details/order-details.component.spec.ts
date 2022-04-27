import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderDetailsComponent } from './order-details.component';

describe('OrderDetailsComponent', () => {
  let component: OrderDetailsComponent;
  let fixture: ComponentFixture<OrderDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrderDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should declare', () => {
    expect(component.closeDetailsSection).toBeTruthy();
  });

  it('should declare', () => {
    expect(component.deliveryInfo).toBeTruthy();
  });

  it('should declare', () => {
    expect(component.detailsToggleEvent).toBeTruthy();
  });

  it('should declare', () => {
    expect(component.totalPrice).toBeTruthy();
  });

  it('should declare', () => {
    expect(component.totalPrice).toBeTruthy();
  });

  it('should declare icons', () => {
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

  it('should declare', () => {
    expect(component.panelOpenState).toBeTruthy();
  });

});
