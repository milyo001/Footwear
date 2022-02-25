import { HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { getBaseUrl } from '../../environments/environment.test';

import { CartService } from './cart.service';

describe('CartService', () => {
  let service: CartService;
  const baseUrl = getBaseUrl();

  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientModule],
    providers: [{ provide: 'BASE_URL', useValue: baseUrl }]
  }));

  it('Service should be created', () => {
    const service: CartService = TestBed.get(CartService);
    expect(service).toBeTruthy();
  });

  it('#addToCart should be created', () => {
    const service: CartService = TestBed.get(CartService);
    expect(service.addToCart).toBeTruthy();
  });

  it('#defaultQuantity should be created', () => {
    const service: CartService = TestBed.get(CartService);
    expect(service.defaultQuantity).toBeTruthy();
  });

  it('should be created', () => {
    const service: CartService = TestBed.get(CartService);
    expect(service).toBeTruthy();
  });

  it('should be created', () => {
    const service: CartService = TestBed.get(CartService);
    expect(service).toBeTruthy();
  });

  it('should be created', () => {
    const service: CartService = TestBed.get(CartService);
    expect(service).toBeTruthy();
  });
});
