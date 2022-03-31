import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { getBaseUrl } from '../../environments/environment.test';
import { ICartProduct } from '../interfaces/cart/cartProduct';
import { asyncData } from '../testing/async-observable-helpers';

import { CartService } from './cart.service';

describe('CartService', () => {
  let service: CartService;
  const baseUrl = getBaseUrl();
  let httpClientSpy: jasmine.SpyObj<HttpClient>;

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

  it('#decreaseProductQuantity should be created', () => {
    const service: CartService = TestBed.get(CartService);
    expect(service.decreaseProductQuantity).toBeTruthy();
  });

  it('#increaseProductQuantity should be created', () => {
    const service: CartService = TestBed.get(CartService);
    expect(service.increaseProductQuantity).toBeTruthy();
  });

  it('#deleteCartProduct should be created', () => {
    const service: CartService = TestBed.get(CartService);
    expect(service.deleteCartProduct).toBeTruthy();
  });

  it('#getAllCartProducts should be created', () => {
    const service: CartService = TestBed.get(CartService);
    expect(service.getAllCartProducts).toBeTruthy();
  });

  it('#removeAllCartProduts should be created', () => {
    const service: CartService = TestBed.get(CartService);
    expect(service.removeAllCartProduts).toBeTruthy();
  });

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get', 'post', 'put', 'delete']);
    service = new CartService(httpClientSpy, getBaseUrl());
  });

  it('#addToCart should return succeeded property(HttpClient called just once)', (done: DoneFn) => {

    const expectedResponse = { succeeded: true };
    const testId: number = 1;
    const testSize: number = 43;

    httpClientSpy.post.and.returnValue(asyncData(expectedResponse));

    service.addToCart(testId, testSize).subscribe({
      next: (response: any) => {
        expect(response.succeeded)
          .withContext('expected succeeded property')
          .toEqual(expectedResponse.succeeded);
        done();
      },
      error: done.fail
    });
    expect(httpClientSpy.post.calls.count())
      .withContext('one call')
      .toBe(1);
  });
});
