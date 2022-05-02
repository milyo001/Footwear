import { ComponentFixture, fakeAsync, TestBed } from '@angular/core/testing';
import { ICartProduct } from 'src/app/interfaces/cart/cartProduct';
import { OrderDetailsProductComponent } from './order-details-product.component';

describe('OrderDetailsProductComponent', () => {
  const product: ICartProduct = {
    size: 20,
    details: 'sadas',
    name: 'test',
    gender: 'undefined',
    price: 20,
    imageUrl: 'google.com/image?=21313asd',
    productId: 1,
    quantity: 2,
    id: 44,
    productType: 'running',
  };
  
  let component: OrderDetailsProductComponent;
  let fixture: ComponentFixture<OrderDetailsProductComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OrderDetailsProductComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderDetailsProductComponent);
    component = fixture.componentInstance;
    component.product = product;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should define @Input #product', () => {
    expect(component.product).toBeDefined();
  });

  it('should be product of type ICartProduct', () => {
    expect(component.product).toEqual(product);
  });
});
