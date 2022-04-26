import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderDetailsProductComponent } from './order-details-product.component';

describe('OrderDetailsProductComponent', () => {
  let component: OrderDetailsProductComponent;
  let fixture: ComponentFixture<OrderDetailsProductComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrderDetailsProductComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderDetailsProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
